using System.Collections.Concurrent;
using Db;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Service;

public class ExportJobPollingService(IServiceProvider _serviceProvider) : BackgroundService
{
    private readonly ConcurrentDictionary<int, int> _jobs = new(); // jobId -> domainId
    private readonly SemaphoreSlim _signal = new(0);

    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("http://localhost:5170")
    };

    public Action<int, int, string>? NotifyJobStatusChange { get; set; }

    public void AddJob(int jobId, int domainId)
    {
        if(_jobs.TryAdd(jobId, domainId))
        {
            _signal.Release();
        }
    }

    private void RemoveJob(int jobId)
    {
        _jobs.TryRemove(jobId, out _);
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await _signal.WaitAsync(token);

            while (!_jobs.IsEmpty && !token.IsCancellationRequested)
            {
                var currentJobs = _jobs.Keys.ToList();

                var tasks = currentJobs
                    .Select(jobId => ProcessJobAsync(jobId, token))
                    .ToList();

                await Task.WhenAll(tasks);

                await Task.Delay(1000, token);
            }
        }
    }

    private async Task ProcessJobAsync(int operationId, CancellationToken token)
    {
        try
        {
            var status = await CheckIfJobCompleteAsync(operationId, token);
            if(status == "Processing") return;

            if(_jobs.TryGetValue(operationId, out var domainId))
            {
                // We're getting a little naughty here by using the service provider to resolve a scoped service
                using var scope = _serviceProvider.CreateScope();
                var exportJobDb = scope.ServiceProvider.GetRequiredService<IExportJobDb>();
                var jobId = exportJobDb.UpdateOperationStatus(operationId, status);
                NotifyJobStatusChange?.Invoke(domainId, jobId, status);
            }

            RemoveJob(operationId);

        } catch(Exception ex)
        {
            await Console.Error.WriteLineAsync($"Error processing job {operationId}: {ex.Message}");
        }
    }

    private async Task<string> CheckIfJobCompleteAsync(int jobId, CancellationToken token)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/mock/api/export-jobs/status?jobId={jobId}", token);

            if(!response.IsSuccessStatusCode)
            {
                return "Processing";
            }

            var body = await response.Content.ReadAsStringAsync(token);

            return body;
        } catch(Exception ex)
        {
            await Console.Error.WriteLineAsync($"Error checking job {jobId}: {ex.Message}");
            return "Processing";
        }
    }
}

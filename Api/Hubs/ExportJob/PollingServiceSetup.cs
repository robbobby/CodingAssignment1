using Microsoft.AspNetCore.SignalR;
using Service;

namespace Api.Hubs.ExportJob;

public static class PollingServiceSetup
{
    public static void ExportJobBootstrap(WebApplication app)
    {
        var pollingService = app.Services.GetRequiredService<ExportJobPollingService>();
        var hubContext = app.Services.GetRequiredService<IHubContext<ExportJobHub>>();

        pollingService.NotifyJobStatusChange = async void (domainId, jobId, status) =>
        {
            try
            {
                var matchingConnections = ExportJobHub.GetConnectionsForDomain(domainId);

                Console.WriteLine($"Notifying domain {domainId} about job {jobId} status change to {status}");
                foreach(var connectionId in matchingConnections)
                {
                    await hubContext.Clients.Client(connectionId).SendAsync("JobUpdate", jobId, status);
                }
            } catch(Exception e)
            {
                Console.WriteLine($"Error notifying job status change: {e.Message}");
            }
        };
    }
}

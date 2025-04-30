using Core.ApiModels;
using Db;

namespace Service;

public interface IExportJobService
{
    void StartExportJob(string dataSet, int domainId);
    Job[] GetAllExportJobs(int domainId);
}

public class ExportJobService(IExportJobDb _exportJobDb, ExportJobPollingService _exportJobPollingService)
    : IExportJobService
{
    public void StartExportJob(string dataSet, int domainId)
    {
        if (string.IsNullOrEmpty(dataSet))
        {
            throw new ArgumentException("Dataset cannot be null or empty.", nameof(dataSet));
        }

        var job = _exportJobDb.StartExportJob(dataSet);

        _exportJobPollingService.AddJob(job.OperationId, domainId);
    }

    public Job[] GetAllExportJobs(int domainId)
    {
        var jobEntity = _exportJobDb.GetAllExportJobs(domainId).ToArray();

        foreach(var job in jobEntity)
        {
            if(job.Status == "Processing")
            {
                _exportJobPollingService.AddJob(job.OperationId, domainId);
            }
        }

        return jobEntity.Select(j => new Job
        {
            Id = j.Id,
            Name = j.Name,
            Status = j.Status,
            CreatedAt = j.CreatedAt
        }).ToArray();
    }
}

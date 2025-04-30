using Core.ApiModels;
using Db;

namespace Service;

public interface IExportJobService
{
    void StartExportJob();
    void StopExportJob();
    Job[] GetAllExportJobs(int domainId);
}

public class ExportJobService(IExportJobDb _exportJobDb, ExportJobPollingService _exportJobPollingService)
    : IExportJobService
{
    public void StartExportJob()
    {
        throw new NotImplementedException();
    }

    public void StopExportJob()
    {
        throw new NotImplementedException();
    }

    public Job[] GetAllExportJobs(int domainId)
    {
        var jobEntity = _exportJobDb.GetAllExportJobs(domainId).ToArray();

        foreach(var job in jobEntity)
        {
            if(job.Status == "Processing")
            {
                Console.WriteLine($"Job {job.Id} is already processing.");
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

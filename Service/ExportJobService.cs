using Core.ApiModels;
using Db;

namespace Service;

public interface IExportJobService
{
    void StartExportJob();
    void StopExportJob();
    Job[] GetAllExportJobs(int domainId);
}

public class ExportJobService(IExportJobDb _exportJobDb) : IExportJobService
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
        var jobs = _exportJobDb.GetAllExportJobs(domainId)
            .Select(j => new Job
            {
                Id = j.Id,
                Name = j.Name,
                Status = j.Status,
                CreatedAt = j.CreatedAt
            }).ToArray();

        foreach(Job job in jobs)
        {
            if(job.Status == "Processing")
            {
                // TODO: start the polling process
            }
        }

        return jobs;
    }
}

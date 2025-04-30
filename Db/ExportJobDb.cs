using Db.Entity;

namespace Db;

public interface IExportJobDb
{
    ExportJobEntity StartExportJob(string dataSet);
    void StopExportJob();
    IQueryable<ExportJobEntity> GetAllExportJobs(int domainId);
    int UpdateOperationStatus(int operationId, string status);
}

public class ExportJobDb : IExportJobDb
{
    private static readonly List<ExportJobEntity> _jobs =
    [
        new() { Id = 1, Name = "Export Job 1", Status = "Processing", CreatedAt = DateTime.Now.AddDays(-2) , OperationId = 11 },
        new() { Id = 2, Name = "Export Job 2", Status = "Processing", CreatedAt = DateTime.Now.AddHours(-1), OperationId = 22 },
        new() { Id = 3, Name = "Export Job 3", Status = "Processing", CreatedAt = DateTime.Now.AddHours(-5) , OperationId = 33 },
    ];

    public ExportJobEntity StartExportJob(string dataSet)
    {
        var job = new ExportJobEntity
        {
            Id = _jobs.Count + 1,
            Name = dataSet + (_jobs.Count + 1),
            Status = "Processing",
            CreatedAt = DateTime.Now,
            OperationId = _jobs.Count + 1
        };

        _jobs.Add(job);
        return job;
    }

    public void StopExportJob()
    {
        throw new NotImplementedException();
    }

    public IQueryable<ExportJobEntity> GetAllExportJobs(int domainId)
    {
        return _jobs.AsQueryable();
    }

    public int UpdateOperationStatus(int operationId, string status)
    {
        var job = _jobs.FirstOrDefault(j => j.OperationId == operationId);
        
        if (job != null)
        {
            job.Status = status;
            return job.Id;
        }
        
        throw new Exception($"Job {operationId} not found");
    }
}

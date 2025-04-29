using Db.Entity;

namespace Db;

public interface IExportJobDb
{
    void StartExportJob();
    void StopExportJob();
    IQueryable<ExportJobEntity> GetAllExportJobs(int domainId);
}

public class ExportJobDb : IExportJobDb
{
    private readonly List<ExportJobEntity> _jobs =
    [
        new ExportJobEntity { Id = 1, Name = "Export Job 1", Status = "Success", CreatedAt = DateTime.Now.AddDays(-2) , OperationId = 11 },
        new ExportJobEntity { Id = 2, Name = "Export Job 2", Status = "Processing", CreatedAt = DateTime.Now.AddHours(-1), OperationId = 22 },
        new ExportJobEntity { Id = 3, Name = "Export Job 3", Status = "Error", CreatedAt = DateTime.Now.AddHours(-5) , OperationId = 33 },
    ];

    public void StartExportJob()
    {
        throw new NotImplementedException();
    }

    public void StopExportJob()
    {
        throw new NotImplementedException();
    }

    public IQueryable<ExportJobEntity> GetAllExportJobs(int domainId)
    {
        return _jobs.AsQueryable();
    }
}

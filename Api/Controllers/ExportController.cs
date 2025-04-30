using Core.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers;

[ApiController]
[Route("/api/export-jobs")]
public class ExportController(IExportJobService _exportJobService) : ControllerBase
{
    // [Authorize]
    [HttpGet]
    public ActionResult<Job[]> Get()
    {
        // jwt or session
        var domainId = 1;
        var jobs = _exportJobService.GetAllExportJobs(1);

        return Ok(jobs);
    }

    // [Authorize]
    [HttpPost("start")]
    public IActionResult Start([FromBody] StartJobRequest job)
    {
        var domainId = 1;
        _exportJobService.StartExportJob(job.DataSet, domainId);

        return Ok();
    }
}

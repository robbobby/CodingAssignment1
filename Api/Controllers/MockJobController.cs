using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class MockJobController : ControllerBase
{
    [HttpGet("/mock/api/export-jobs/status")]
    public IActionResult GetJobStatus([FromQuery] int jobId)
    {
        var number = new Random().Next(0, 10);
        return Ok(number switch
        {
            9 => "Success",
            0 => "Error",
            _ => "Processing"
        });
    }
}

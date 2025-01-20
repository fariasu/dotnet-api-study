using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("")]
public class HealthCheckController : ControllerBase
{
    [HttpGet("")]
    public IActionResult HealthCheck()
    {
        return Ok("It's working!");
    }
}
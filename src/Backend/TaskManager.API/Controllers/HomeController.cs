using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("/health-check")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult HealthCheck()
    {
        return Ok("It's working!");
    }
}
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Create;
using TaskManager.Communication.DTOs.Request;
using TaskManager.Communication.DTOs.Response;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatedTaskJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTask([FromServices] ICreateTaskUseCase useCase,
        [FromBody] RequestCreateTaskJson request)
    {
        var response = await useCase.Execute(request);

        return Created("", response);
    }
}
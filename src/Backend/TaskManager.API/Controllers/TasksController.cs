using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Tasks.Create;
using TaskManager.Application.UseCases.Tasks.Delete;
using TaskManager.Application.UseCases.Tasks.GetAll;
using TaskManager.Application.UseCases.Tasks.GetById;
using TaskManager.Application.UseCases.Tasks.Update;
using TaskManager.Communication.DTOs.Tasks.Request;
using TaskManager.Communication.DTOs.Tasks.Response;

namespace TaskManager.API.Controllers;


[Route("[controller]")]
[ApiController]
[Authorize]
public class TasksController : ControllerBase
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(ResponseCreatedTaskJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateTask([FromServices] ICreateTaskUseCase useCase,
        [FromBody] RequestTaskJson request)
    {
        var response = await useCase.Execute(request);

        return Created("", response);
    }

    [HttpGet("get")]
    [ProducesResponseType(typeof(ResponseTasksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTasks([FromServices] IGetAllTasksUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Tasks.Count == 0) 
            return NoContent();
        
        return Ok(response);
    }

    [HttpGet("get/{id:long}")]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut("update/{id:long}")]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateTaskById([FromServices] IUpdateTaskUseCase useCase, [FromRoute] long id, [FromBody] RequestTaskJson request)
    {
        await useCase.Execute(id, request);
        
        return NoContent();
    }

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteById([FromServices] IDeleteTaskUseCase useCase, [FromRoute] long id)
    {
        await useCase.Execute(id);
        
        return NoContent();
    }
}
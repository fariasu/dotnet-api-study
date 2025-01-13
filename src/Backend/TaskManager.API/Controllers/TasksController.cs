using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Create;
using TaskManager.Application.UseCases.Delete;
using TaskManager.Application.UseCases.GetAll;
using TaskManager.Application.UseCases.GetById;
using TaskManager.Application.UseCases.Update;
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
        [FromBody] RequestTaskJson request)
    {
        var response = await useCase.Execute(request);

        return Created("", response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTasksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetTasks([FromServices] IGetAllTasksUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Tasks.Count == 0) 
            return NoContent();
        
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdUseCase useCase, [FromRoute] int id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ResponseTaskJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTaskById([FromServices] IUpdateTaskUseCase useCase, [FromRoute] int id, [FromBody] RequestTaskJson request)
    {
        await useCase.Execute(id, request);
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteById([FromServices] IDeleteTaskUseCase useCase, [FromRoute] int id)
    {
        await useCase.Execute(id);
        
        return NoContent();
    }
}
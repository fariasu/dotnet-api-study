using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Users.Create;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Communication.DTOs.Users.Responses;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUserCreatedJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser([FromServices] ICreateUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
    {
         var response = await useCase.Execute(request); 
         
         return Created("", response);
    }
}
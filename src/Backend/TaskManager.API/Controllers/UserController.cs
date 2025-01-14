﻿using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Users.Create;
using TaskManager.Application.UseCases.Users.Login;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Communication.DTOs.Users.Responses;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseCreatedUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser([FromServices] ICreateUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
    {
         var response = await useCase.Execute(request); 
         
         return Created("", response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseCreatedUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginUser([FromServices] ILoginUserUseCase useCase, [FromBody] RequestLoginUserJson request)
    {
        var result = await useCase.Execute(request);
        
        return Ok(result);
    }
}
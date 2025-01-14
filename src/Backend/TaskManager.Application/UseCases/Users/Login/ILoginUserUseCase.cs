using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Communication.DTOs.Users.Responses;

namespace TaskManager.Application.UseCases.Users.Login;

public interface ILoginUserUseCase
{
    public Task<ResponseCreatedUserJson> Execute(RequestLoginUserJson request);
}
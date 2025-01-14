using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Communication.DTOs.Users.Responses;

namespace TaskManager.Application.UseCases.Users.Create;

public interface ICreateUserUseCase
{
    public Task<ResponseUserCreatedJson> Execute(RequestRegisterUserJson request);
}
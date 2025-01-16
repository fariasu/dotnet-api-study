using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Communication.DTOs.Users.Responses;

namespace TaskManager.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    public Task<ResponseCreatedUserJson> Execute(RequestRegisterUserJson request);
}
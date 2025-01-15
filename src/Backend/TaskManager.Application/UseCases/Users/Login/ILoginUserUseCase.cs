using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Communication.DTOs.Users.Requests;

namespace TaskManager.Application.UseCases.Users.Login;

public interface ILoginUserUseCase
{
    public Task<ResponseLoggedUserJson> Execute(RequestLoginUserJson request);
}
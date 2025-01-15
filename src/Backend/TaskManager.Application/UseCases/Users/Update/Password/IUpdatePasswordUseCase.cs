using TaskManager.Communication.DTOs.Users.Requests;

namespace TaskManager.Application.UseCases.Users.Update.Password;

public interface IUpdatePasswordUseCase
{
    public Task Execute(RequestUpdatePasswordJson request);
}
using TaskManager.Communication.DTOs.Users.Requests;

namespace TaskManager.Application.UseCases.Users.Update.Profile;

public interface IUpdateProfileUseCase
{
    public Task Execute(RequestUpdateProfileJson request);
}
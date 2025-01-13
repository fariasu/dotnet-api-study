using TaskManager.Communication.DTOs.Response;

namespace TaskManager.Application.UseCases.GetAll;

public interface IGetAllTasksUseCase
{
    public Task<ResponseTasksJson> Execute();
}
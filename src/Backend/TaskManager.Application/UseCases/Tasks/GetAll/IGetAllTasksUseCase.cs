using TaskManager.Communication.DTOs.Tasks.Request;
using TaskManager.Communication.DTOs.Tasks.Response;

namespace TaskManager.Application.UseCases.Tasks.GetAll;

public interface IGetAllTasksUseCase
{
    public Task<ResponseTasksJson> Execute(RequestTasksJson request);
}
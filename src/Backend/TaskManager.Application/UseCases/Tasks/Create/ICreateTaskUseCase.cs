using TaskManager.Communication.DTOs.Tasks.Request;
using TaskManager.Communication.DTOs.Tasks.Response;

namespace TaskManager.Application.UseCases.Tasks.Create;

public interface ICreateTaskUseCase
{
    public Task<ResponseCreatedTaskJson> Execute(RequestTaskJson request);
}
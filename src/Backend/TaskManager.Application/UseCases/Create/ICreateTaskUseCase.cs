using TaskManager.Communication.DTOs.Request;
using TaskManager.Communication.DTOs.Response;

namespace TaskManager.Application.UseCases.Create;

public interface ICreateTaskUseCase
{
    public Task<ResponseCreatedTaskJson> Execute(RequestTaskJson request);
}
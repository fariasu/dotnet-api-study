using TaskManager.Communication.DTOs.Tasks.Request;

namespace TaskManager.Application.UseCases.Tasks.Update;

public interface IUpdateTaskUseCase
{
    public Task Execute(long id, RequestTaskJson requestTask);
}
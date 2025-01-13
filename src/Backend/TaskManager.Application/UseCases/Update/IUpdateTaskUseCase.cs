using TaskManager.Communication.DTOs.Request;

namespace TaskManager.Application.UseCases.Update;

public interface IUpdateTaskUseCase
{
    public Task Execute(int id, RequestTaskJson requestTask);
}
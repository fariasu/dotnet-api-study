using TaskManager.Communication.DTOs.Tasks.Response;

namespace TaskManager.Application.UseCases.Tasks.GetById;

public interface IGetByIdUseCase
{
    public Task<ResponseTaskJson> Execute(long id);
}
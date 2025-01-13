using TaskManager.Communication.DTOs.Response;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.UseCases.GetById;

public interface IGetByIdUseCase
{
    public Task<ResponseTaskJson> Execute(int id);
}
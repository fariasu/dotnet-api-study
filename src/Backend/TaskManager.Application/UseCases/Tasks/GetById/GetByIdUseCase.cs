using AutoMapper;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Services;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Tasks.GetById;

public class GetByIdUseCase(
    IMapper mapper,
    ITaskRepositoryReadOnly taskRepositoryReadOnly,
    ILoggedUserService loggedUserService)
    : IGetByIdUseCase
{
    
    public async Task<ResponseTaskJson> Execute(long id)
    {
        var creatorId = loggedUserService.User().Result.Id;
        
        var result = await taskRepositoryReadOnly.GetByIdNoTracking(id, creatorId);

        if (result == null)
            throw new NotFoundException("Task not found.");
        
        return mapper.Map<ResponseTaskJson>(result);
    }
}
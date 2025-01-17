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
        var loggedUser = await loggedUserService.GetUserAsync();
        
        var result = await taskRepositoryReadOnly.GetByIdNoTracking(id, loggedUser.Id);

        if (result == null)
            throw new NotFoundException("Task not found.");
        
        return mapper.Map<ResponseTaskJson>(result);
    }
}
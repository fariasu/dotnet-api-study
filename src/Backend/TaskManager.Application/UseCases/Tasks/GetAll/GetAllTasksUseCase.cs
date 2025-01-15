using AutoMapper;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Services;

namespace TaskManager.Application.UseCases.Tasks.GetAll;

public class GetAllTasksUseCase(
    IMapper mapper,
    ITaskRepositoryReadOnly repositoryReadOnly,
    ILoggedUserService loggedUserService)
    : IGetAllTasksUseCase
{
    
    public async Task<ResponseTasksJson> Execute()
    {
        var creatorId = loggedUserService.User().Result.Id;
        
        var result = await repositoryReadOnly.GetAll(creatorId);
        
        return new ResponseTasksJson()
        {
            Tasks = mapper.Map<List<ResponseTaskShortJson>>(result)
        };
    }
}
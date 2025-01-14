using AutoMapper;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Services;

namespace TaskManager.Application.UseCases.Tasks.GetAll;

public class GetAllTasksUseCase : IGetAllTasksUseCase
{
    private readonly IMapper _mapper;
    private readonly ITaskRepositoryReadOnly _repositoryReadOnly;
    private readonly ILoggedUserService _loggedUserService;

    public GetAllTasksUseCase(IMapper mapper, 
        ITaskRepositoryReadOnly repositoryReadOnly,
        ILoggedUserService loggedUserService)
    {
        _mapper = mapper;
        _repositoryReadOnly = repositoryReadOnly;
        _loggedUserService = loggedUserService;
    }

    public async Task<ResponseTasksJson> Execute()
    {
        var creatorId = _loggedUserService.User().Result.Id;
        
        var result = await _repositoryReadOnly.GetAll(creatorId);
        
        return new ResponseTasksJson()
        {
            Tasks = _mapper.Map<List<ResponseTaskShortJson>>(result)
        };
    }
}
using AutoMapper;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Services;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Tasks.GetById;

public class GetByIdUseCase : IGetByIdUseCase
{
    private readonly IMapper _mapper;
    private readonly ITaskRepositoryReadOnly _taskRepositoryReadOnly;
    private readonly ILoggedUserService _loggedUserService;
    
    public GetByIdUseCase(IMapper mapper,
        ITaskRepositoryReadOnly taskRepositoryReadOnly,
        ILoggedUserService loggedUserService)
    {
        _mapper = mapper;
        _taskRepositoryReadOnly = taskRepositoryReadOnly;
        _loggedUserService = loggedUserService;
    }
    
    public async Task<ResponseTaskJson> Execute(long id)
    {
        var creatorId = _loggedUserService.User().Result.Id;
        
        var result = await _taskRepositoryReadOnly.GetByIdNoTracking(id, creatorId);

        if (result == null)
            throw new NotFoundException("Task not found.");
        
        return _mapper.Map<ResponseTaskJson>(result);
    }
}
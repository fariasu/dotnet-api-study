using AutoMapper;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Tasks.GetById;

public class GetByIdUseCase : IGetByIdUseCase
{
    private readonly IMapper _mapper;
    private readonly ITaskRepositoryReadOnly _taskRepositoryReadOnly;
    
    public GetByIdUseCase(IMapper mapper, ITaskRepositoryReadOnly taskRepositoryReadOnly)
    {
        _mapper = mapper;
        _taskRepositoryReadOnly = taskRepositoryReadOnly;
    }
    
    public async Task<ResponseTaskJson> Execute(long id)
    {
        var result = await _taskRepositoryReadOnly.GetByIdNoTracking(id);

        if (result == null)
            throw new NotFoundException("Task not found.");
        
        return _mapper.Map<ResponseTaskJson>(result);
    }
}
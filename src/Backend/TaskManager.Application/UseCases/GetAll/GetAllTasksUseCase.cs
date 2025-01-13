using AutoMapper;
using TaskManager.Communication.DTOs.Response;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;

namespace TaskManager.Application.UseCases.GetAll;

public class GetAllTasksUseCase : IGetAllTasksUseCase
{
    private readonly IMapper _mapper;
    private readonly ITaskRepositoryReadOnly _repositoryReadOnly;

    public GetAllTasksUseCase(IMapper mapper, ITaskRepositoryReadOnly repositoryReadOnly)
    {
        _mapper = mapper;
        _repositoryReadOnly = repositoryReadOnly;
    }

    public async Task<ResponseTasksJson> Execute()
    {
        var result = await _repositoryReadOnly.GetAll();
        
        return new ResponseTasksJson()
        {
            Tasks = _mapper.Map<List<ResponseTaskShortJson>>(result)
        };
    }
}
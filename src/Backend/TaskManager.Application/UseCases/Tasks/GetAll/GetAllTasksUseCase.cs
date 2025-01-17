using AutoMapper;
using TaskManager.Communication.DTOs.Tasks.Request;
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
    
    public async Task<ResponseTasksJson> Execute(RequestTasksJson request)
    {
        var (page, pageSize) = Validate(request);
        
        var loggedUser = await loggedUserService.GetUserAsync();
        
        var result = await repositoryReadOnly.GetAll(loggedUser.Id, page, pageSize);
        
        return new ResponseTasksJson()
        {
            Tasks = mapper.Map<List<ResponseTaskShortJson>>(result)
        };
    }

    private (int Page, int PageSize) Validate(RequestTasksJson request)
    {
        var page = request.Page < 0 ? 0 : request.Page;
        var pageSize = request.PageSize < 5 ? 5 : request.PageSize;
        return (page, pageSize);
    }
}
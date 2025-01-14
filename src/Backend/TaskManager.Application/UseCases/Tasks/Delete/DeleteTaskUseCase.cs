using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Services;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Tasks.Delete;

public class DeleteTaskUseCase : IDeleteTaskUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepositoryWriteOnly _repositoryWriteOnly;
    private readonly ITaskRepositoryReadOnly _repositoryReadOnly;
    private readonly ILoggedUserService _loggedUserService;

    public DeleteTaskUseCase(IUnitOfWork unitOfWork, 
        ITaskRepositoryWriteOnly repositoryWriteOnly,
        ITaskRepositoryReadOnly repositoryReadOnly,
        ILoggedUserService loggedUserService)
    {
        _unitOfWork = unitOfWork;
        _repositoryWriteOnly = repositoryWriteOnly;
        _repositoryReadOnly = repositoryReadOnly;
        _loggedUserService = loggedUserService;
    }

    public async Task Execute(long id)
    {
        var creatorId = _loggedUserService.User().Result.Id;

        var result = await _repositoryReadOnly.GetById(id, creatorId);

        if (result == null)
            throw new NotFoundException("Task not found.");

        _repositoryWriteOnly.DeleteTask(result);
        
        await _unitOfWork.CommitAsync();
    }
}
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Services;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Tasks.Delete;

public class DeleteTaskUseCase(
    IUnitOfWork unitOfWork,
    ITaskRepositoryWriteOnly repositoryWriteOnly,
    ITaskRepositoryReadOnly repositoryReadOnly,
    ILoggedUserService loggedUserService)
    : IDeleteTaskUseCase
{
    
    public async Task Execute(long id)
    {
        var creatorId = loggedUserService.User().Result.Id;

        var result = await repositoryReadOnly.GetById(id, creatorId);

        if (result == null)
            throw new NotFoundException("Task not found.");

        repositoryWriteOnly.DeleteTask(result);
        
        await unitOfWork.CommitAsync();
    }
}
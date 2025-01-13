using AutoMapper;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Delete;

public class DeleteTaskUseCase : IDeleteTaskUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepositoryWriteOnly _repositoryWriteOnly;
    private readonly ITaskRepositoryReadOnly _repositoryReadOnly;

    public DeleteTaskUseCase(IUnitOfWork unitOfWork, ITaskRepositoryWriteOnly repositoryWriteOnly,
        ITaskRepositoryReadOnly repositoryReadOnly)
    {
        _unitOfWork = unitOfWork;
        _repositoryWriteOnly = repositoryWriteOnly;
        _repositoryReadOnly = repositoryReadOnly;
    }

    public async Task Execute(int id)
    {
        var result = await _repositoryReadOnly.GetById(id);

        if (result == null)
            throw new NotFoundException("Task not found.");

        _repositoryWriteOnly.DeleteTask(result);
        
        await _unitOfWork.CommitAsync();
    }
}
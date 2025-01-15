using AutoMapper;
using TaskManager.Application.Validators.Tasks;
using TaskManager.Communication.DTOs.Tasks.Request;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Services;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Tasks.Update;

public class UpdateTaskUseCase(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ITaskRepositoryUpdateOnly repositoryUpdateOnly,
    ITaskRepositoryReadOnly repositoryReadOnly,
    ILoggedUserService loggedUserService)
    : IUpdateTaskUseCase
{
    
    public async Task Execute(long id, RequestTaskJson requestTask)
    {
        await Validate(requestTask);

        var creatorId = loggedUserService.User().Result.Id;
        
        var result = await repositoryReadOnly.GetById(id, creatorId);
        
        if(result == null)
            throw new NotFoundException("Task not found.");
        
        mapper.Map(requestTask, result);
        
        repositoryUpdateOnly.Update(result);
        
        await unitOfWork.CommitAsync();
    }

    private async Task Validate(RequestTaskJson request)
    {
        var validator = new TaskValidator();

        var result = await validator.ValidateAsync(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
using AutoMapper;
using TaskManager.Application.Validators.Tasks;
using TaskManager.Communication.DTOs.Tasks.Request;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Services;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Tasks.Create;

public class CreateTaskUseCase(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ITaskRepositoryWriteOnly
        taskRepositoryWriteOnly,
    ILoggedUserService loggedUserService)
    : ICreateTaskUseCase
{
    
    public async Task<ResponseCreatedTaskJson> Execute(RequestTaskJson request)
    {
        await Validate(request);
        
        var taskEntity = mapper.Map<TaskEntity>(request);
        taskEntity.CreatorId = loggedUserService.User().Result.Id;

        await taskRepositoryWriteOnly.CreateTask(taskEntity);

        await unitOfWork.CommitAsync();
        
        return mapper.Map<ResponseCreatedTaskJson>(taskEntity);
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
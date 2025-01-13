using AutoMapper;
using TaskManager.Application.Validators.Task;
using TaskManager.Communication.DTOs.Request;
using TaskManager.Communication.DTOs.Response;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Task;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Create;

public class CreateTaskUseCase : ICreateTaskUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepositoryWriteOnly _repositoryWriteOnly;

    public CreateTaskUseCase(IMapper mapper, IUnitOfWork unitOfWork, ITaskRepositoryWriteOnly taskRepositoryWriteOnly)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repositoryWriteOnly = taskRepositoryWriteOnly;
    }

    public async Task<ResponseCreatedTaskJson> Execute(RequestCreateTaskJson request)
    {
        await Validate(request);
        
        var map = _mapper.Map<TaskEntity>(request);
        
        await _repositoryWriteOnly.CreateTask(map);

        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseCreatedTaskJson>(map);
    }
    
    private async Task Validate(RequestCreateTaskJson request)
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
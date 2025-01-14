using AutoMapper;
using TaskManager.Application.Validators.Tasks;
using TaskManager.Communication.DTOs.Tasks.Request;
using TaskManager.Communication.DTOs.Tasks.Response;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Tasks.Create;

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

    public async Task<ResponseCreatedTaskJson> Execute(RequestTaskJson request)
    {
        await Validate(request);
        
        var map = _mapper.Map<TaskEntity>(request);
        
        await _repositoryWriteOnly.CreateTask(map);

        await _unitOfWork.CommitAsync();
        
        return _mapper.Map<ResponseCreatedTaskJson>(map);
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
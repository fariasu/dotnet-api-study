using AutoMapper;
using TaskManager.Application.Validators.Task;
using TaskManager.Communication.DTOs.Request;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Update;

public class UpdateTaskUseCase : IUpdateTaskUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepositoryUpdateOnly _repositoryUpdateOnly;
    private readonly ITaskRepositoryReadOnly _repositoryReadOnly;

    public UpdateTaskUseCase(IMapper mapper, IUnitOfWork unitOfWork, ITaskRepositoryUpdateOnly repositoryUpdateOnly,
        ITaskRepositoryReadOnly repositoryReadOnly)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repositoryUpdateOnly = repositoryUpdateOnly;
        _repositoryReadOnly = repositoryReadOnly;
    }
    
    public async Task Execute(int id, RequestTaskJson requestTask)
    {
        await Validate(requestTask);
        
        var result = await _repositoryReadOnly.GetById(id);
        
        if(result == null)
            throw new NotFoundException("Task not found.");
        
        _mapper.Map(requestTask, result);
        
        _repositoryUpdateOnly.Update(result);
        
        await _unitOfWork.CommitAsync();
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
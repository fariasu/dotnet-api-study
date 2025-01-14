using AutoMapper;
using TaskManager.Application.Validators.Tasks;
using TaskManager.Communication.DTOs.Tasks.Request;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Domain.Services;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Tasks.Update;

public class UpdateTaskUseCase : IUpdateTaskUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepositoryUpdateOnly _repositoryUpdateOnly;
    private readonly ITaskRepositoryReadOnly _repositoryReadOnly;
    private readonly ILoggedUserService _loggedUserService;

    public UpdateTaskUseCase(IMapper mapper, 
        IUnitOfWork unitOfWork, 
        ITaskRepositoryUpdateOnly repositoryUpdateOnly,
        ITaskRepositoryReadOnly repositoryReadOnly,
        ILoggedUserService loggedUserService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repositoryUpdateOnly = repositoryUpdateOnly;
        _repositoryReadOnly = repositoryReadOnly;
        _loggedUserService = loggedUserService;
    }
    
    public async Task Execute(long id, RequestTaskJson requestTask)
    {
        await Validate(requestTask);

        var creatorId = _loggedUserService.User().Result.Id;
        
        var result = await _repositoryReadOnly.GetById(id, creatorId);
        
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
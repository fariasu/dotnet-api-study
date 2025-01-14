using AutoMapper;
using FluentValidation;
using TaskManager.Application.Validators.Users;
using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Users;
using TaskManager.Domain.Services;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Users.Update;

public class UpdateProfileUseCase : IUpdateProfileUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepositoryWriteOnly _userRepositoryWriteOnly;
    private readonly IUserRepositoryReadOnly _userRepositoryReadOnly;
    private readonly ILoggedUserService _loggedUserService;

    public UpdateProfileUseCase(IMapper mapper, 
        IUnitOfWork unitOfWork, 
        IUserRepositoryWriteOnly userRepositoryWriteOnly, 
        IUserRepositoryReadOnly userRepositoryReadOnly,
        ILoggedUserService loggedUserService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepositoryWriteOnly = userRepositoryWriteOnly;
        _userRepositoryReadOnly = userRepositoryReadOnly;
        _loggedUserService = loggedUserService;
    }
    
    public async Task Execute(RequestUpdateProfileJson request)
    {
        await Validate(request);
        
        var loggedUserEmail = _loggedUserService.User().Result.Email;
        var userEntity = await _userRepositoryReadOnly.GetActiveUserWithEmail(loggedUserEmail);
        
        _mapper.Map(request, userEntity);

        await _unitOfWork.CommitAsync();
    }

    private async Task Validate(RequestUpdateProfileJson request)
    {
        var result = await new UpdateProfileValidator().ValidateAsync(request);

        if (result.IsValid is false)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
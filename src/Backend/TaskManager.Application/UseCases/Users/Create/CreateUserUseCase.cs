using AutoMapper;
using FluentValidation.Results;
using TaskManager.Application.Validators.Users;
using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Communication.DTOs.Users.Responses;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Users;
using TaskManager.Domain.Security.Cryptography;
using TaskManager.Domain.Security.Tokens;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Users.Create;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepositoryWriteOnly _userRepositoryWriteOnly;
    private readonly IUserRepositoryReadOnly _userRepositoryReadOnly;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly ITokenGenerator _tokenGenerator;

    public CreateUserUseCase(IMapper mapper, 
        IUnitOfWork unitOfWork, 
        IUserRepositoryWriteOnly userRepositoryWriteOnly, 
        IUserRepositoryReadOnly userRepositoryReadOnly,
        IPasswordEncrypter passwordEncrypter,
        ITokenGenerator tokenGenerator)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepositoryWriteOnly = userRepositoryWriteOnly;
        _userRepositoryReadOnly = userRepositoryReadOnly;
        _passwordEncrypter = passwordEncrypter;
        _tokenGenerator = tokenGenerator;
    }
    
    public async Task<ResponseCreatedUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var userEntity = _mapper.Map<UserEntity>(request);
        userEntity.Password = _passwordEncrypter.Encrypt(request.Password);
        userEntity.UserIdentifier = Guid.NewGuid();
        
        await _userRepositoryWriteOnly.CreateUser(userEntity);
        
        await _unitOfWork.CommitAsync();

        var generatedToken = _tokenGenerator.GenerateToken(userEntity);

        return new ResponseCreatedUserJson()
        {
            Name = userEntity.Name,
            Token = generatedToken
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = await new RegisterUserValidator().ValidateAsync(request);
        
        var mailExists = await _userRepositoryReadOnly.ExistsActiveUserWithEmail(request.Email);
        if (mailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Email is already taken."));
        }

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
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

public class CreateUserUseCase(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserRepositoryWriteOnly userRepositoryWriteOnly,
    IUserRepositoryReadOnly userRepositoryReadOnly,
    IPasswordEncrypter passwordEncrypter,
    ITokenGenerator tokenGenerator)
    : ICreateUserUseCase
{
    public async Task<ResponseCreatedUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var userEntity = mapper.Map<UserEntity>(request);
        userEntity.Password = passwordEncrypter.Encrypt(request.Password);
        userEntity.UserIdentifier = Guid.NewGuid();
        
        await userRepositoryWriteOnly.CreateUser(userEntity);
        
        await unitOfWork.CommitAsync();

        var generatedToken = tokenGenerator.GenerateToken(userEntity);

        return new ResponseCreatedUserJson()
        {
            Name = userEntity.Name,
            Token = generatedToken
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = await new RegisterUserValidator().ValidateAsync(request);
        
        var mailExists = await userRepositoryReadOnly.ExistsActiveUserWithEmail(request.Email);
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
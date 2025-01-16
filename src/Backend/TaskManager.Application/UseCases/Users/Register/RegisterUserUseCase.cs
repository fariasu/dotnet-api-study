using AutoMapper;
using FluentValidation.Results;
using TaskManager.Application.Validators.Users;
using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Communication.DTOs.Users.Responses;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Users;
using TaskManager.Domain.Security.Cryptography;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Users.Register;

public class RegisterUserUseCase(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserRepositoryWriteOnly userRepositoryWriteOnly,
    IUserRepositoryReadOnly userRepositoryReadOnly,
    IPasswordEncrypter passwordEncrypter)
    : IRegisterUserUseCase
{
    public async Task<ResponseCreatedUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var userEntity = mapper.Map<UserEntity>(request);
        userEntity.Password = passwordEncrypter.Encrypt(request.Password);
        userEntity.UserIdentifier = Guid.NewGuid();
        
        await userRepositoryWriteOnly.CreateUser(userEntity);
        
        await unitOfWork.CommitAsync();


        return new ResponseCreatedUserJson
        {
            Name = userEntity.Name,
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
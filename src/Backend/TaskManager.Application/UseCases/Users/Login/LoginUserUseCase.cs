using AutoMapper;
using TaskManager.Application.Validators.Users;
using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Communication.DTOs.Users.Responses;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Users;
using TaskManager.Domain.Security.Cryptography;
using TaskManager.Domain.Security.Tokens;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Users.Login;

public class LoginUserUseCase(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserRepositoryReadOnly userRepositoryReadOnly,
    IPasswordEncrypter passwordEncrypter,
    ITokenGenerator tokenGenerator)
    : ILoginUserUseCase
{
    
    public async Task<ResponseCreatedUserJson> Execute(RequestLoginUserJson request)
    {
        var entity = await Validate(request);

        return new ResponseCreatedUserJson()
        {
            Name = entity.Name,
            Token = tokenGenerator.GenerateToken(entity),
        };
    }

    private async Task<UserEntity> Validate(RequestLoginUserJson request)
    {
        var result = await new LoginUserValidator().ValidateAsync(request);
        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
        
        var userEntity = await userRepositoryReadOnly.GetActiveUserWithEmail(request.Email);
        if (userEntity is null)
        {
            throw new InvalidLoginException();
        }
        
        var passwordMatch = passwordEncrypter.Verify(request.Password, userEntity.Password);
        if (passwordMatch is false)
        {
            throw new InvalidLoginException();
        }

        return userEntity;
    }
}
using AutoMapper;
using TaskManager.Application.Validators.Users;
using TaskManager.Communication.DTOs.Users.Requests;
using TaskManager.Domain.Repositories.Db;
using TaskManager.Domain.Repositories.Users;
using TaskManager.Domain.Security.Cryptography;
using TaskManager.Domain.Services;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.Application.UseCases.Users.Update.Password;

public class UpdatePasswordUseCase(
    IUnitOfWork unitOfWork,
    IUserRepositoryWriteOnly userRepositoryWriteOnly,
    IUserRepositoryReadOnly userRepositoryReadOnly,
    ILoggedUserService loggedUserService,
    IPasswordEncrypter passwordEncrypter)
    : IUpdatePasswordUseCase
{
    public async Task Execute(RequestUpdatePasswordJson request)
    {
        await Validate(request);
        
        var loggedUser = await loggedUserService.GetUserAsync();
        var dbUserEntity = await userRepositoryReadOnly.GetActiveUserWithEmail(loggedUser.Email);
        
        var encryptedPassword = passwordEncrypter.Encrypt(request.Password);
        dbUserEntity!.Password = encryptedPassword;

        userRepositoryWriteOnly.UpdatePassword(dbUserEntity);

        await unitOfWork.CommitAsync();
    }

    private async Task Validate(RequestUpdatePasswordJson request)
    {
        var result = await new UpdatePasswordValidator().ValidateAsync(request);

        if (result.IsValid is false)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
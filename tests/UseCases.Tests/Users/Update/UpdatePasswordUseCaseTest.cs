using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Services;
using CommonTestUtilities.Users.Requests;
using FluentAssertions;
using TaskManager.Application.UseCases.Users.Update.Password;
using TaskManager.Domain.Entities;
using TaskManager.Exception.ExceptionsBase;

namespace UseCases.Tests.Users.Update;

public class UpdatePasswordUseCaseTest
{
    [Fact]
    public async Task UpdatePassword_ValidData_Success()
    {
        //Arrange
        var request = RequestUpdatePasswordJsonBuilder.Build();
        var userEntity = new UserEntity()
        {
            Name = "Test",
            Email = "test@test.com",
            Password = request.Password,
        };
        var useCase = CreateUseCase(userEntity);
        
        //Act
        var act = () => useCase.Execute(request);
        
        //Assert
        await act.Should().NotThrowAsync();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaaaaaa")]
    [InlineData("aaaaaaaA")]
    [InlineData("aaaaaaA1")]
    [InlineData("@1aaaaaa")]
    public async Task UpdatePassword_InvalidPassword_Error(string password)
    {
        //Arrange
        var request = RequestUpdatePasswordJsonBuilder.Build();
        request.Password = password;
        var userEntity = new UserEntity()
        {
            Name = "Test",
            Email = "test@test.com",
            Password = password,
        };
        var useCase = CreateUseCase(userEntity);
        
        //Act
        var act = () => useCase.Execute(request);
        
        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(error => error.GetErrors().Count == 1);
    }
    
    private UpdatePasswordUseCase CreateUseCase(UserEntity? userEntity = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeOnlyRepository = UserRepositoryWriteOnlyBuilder.Build();
        var readOnlyRepository = new UserRepositoryReadOnlyBuilder();
        var loggedUserService = new LoggedUserServiceBuilder();
        var passwordEncrypter = new PasswordEncrypterBuilder();
        
        if (userEntity is not null)
        {
            readOnlyRepository.GetActiveUserWithEmail(userEntity);
            loggedUserService.User(userEntity);
        }
        
        return new UpdatePasswordUseCase(unitOfWork, writeOnlyRepository, readOnlyRepository.Build(), loggedUserService.Build(), passwordEncrypter.Build());
    }
}
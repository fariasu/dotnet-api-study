using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Users.Requests;
using FluentAssertions;
using TaskManager.Application.UseCases.Users.Login;
using TaskManager.Domain.Entities;
using TaskManager.Exception.ExceptionsBase;

namespace UseCases.Tests.Users.Login;

public class LoginUserUseCaseTest
{
    [Fact]
    public async Task LoginUser_ValidData_Success()
    {
        //Arrange
        var request = RequestLoginUserJsonBuilder.Build();
        var userEntity = new UserEntity()
        {
            Name = request.Email.Split("@")[0],
            Email = request.Email,
            Password = request.Password,
        };
        var useCase = CreateUseCase(userEntity);

        //Act
        var result = await useCase.Execute(request);

        //Assert
        result.Name.Should().NotBeNullOrEmpty();
        result.Token.Should().NotBeNullOrEmpty();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("test.test")]
    [InlineData("@test.com")]
    public async Task LoginUser_InvalidEmail_Error(string email)
    {
        //Arrange
        var request = RequestLoginUserJsonBuilder.Build();
        var userEntity = new UserEntity()
        {
            Name = request.Email.Split("@")[0],
            Email = email,
            Password = request.Password,
        };
        var useCase = CreateUseCase(userEntity);

        //Act
        var act = () => useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(error => error.GetErrors().Count == 1);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaaaaaa")]
    [InlineData("aaaaaaaA")]
    [InlineData("aaaaaaA1")]
    [InlineData("@1aaaaaa")]
    public async Task LoginUser_InvalidPassword_Error(string password)
    {
        //Arrange
        var request = RequestLoginUserJsonBuilder.Build();
        var userEntity = new UserEntity()
        {
            Name = request.Email.Split("@")[0],
            Email = request.Email,
            Password = password,
        };
        var useCase = CreateUseCase(userEntity);

        //Act
        var act = () => useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(error => error.GetErrors().Count == 1);
    }
    
    private LoginUserUseCase CreateUseCase(UserEntity? userEntity = null)
    {
        var tokenGenerator = TokenGeneratorBuilder.Build();
        var passwordEncrypter = new PasswordEncrypterBuilder();
        var readOnlyRepository = new UserRepositoryReadOnlyBuilder();
        
        if (userEntity is not null)
        {
            readOnlyRepository.GetActiveUserWithEmail(userEntity);
            passwordEncrypter.Verify(userEntity.Password);
        }
        
        return new LoginUserUseCase(readOnlyRepository.Build(), passwordEncrypter.Build(), tokenGenerator);
    }
}
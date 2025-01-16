using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Users.Requests;
using FluentAssertions;
using TaskManager.Application.UseCases.Users.Register;
using TaskManager.Exception.ExceptionsBase;

namespace UseCases.Tests.Users.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task RegisterUser_ValidData_Success()
    {
        //Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        //Act
        var result = await useCase.Execute(request);

        //Assert
        result.Name.Should().Be(request.Name);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task RegisterUser_InvalidName_Error(string name)
    {
        //Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();
        request.Name = name;

        //Act
        var act = async() => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exception => exception.GetErrors().Count == 1);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("test.test")]
    [InlineData("@test.com")]
    public async Task RegisterUser_InvalidEmail_Error(string email)
    {
        //Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();
        request.Email = email;

        //Act
        var act = async() => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exception => exception.GetErrors().Count == 1 || exception.GetErrors().Count == 2);
    }
    
    [Fact]
    public async Task RegisterUser_EmailAlreadyExists_Error()
    {
        //Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        //Act
        var act = async() => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exception => exception.GetErrors().Count == 1);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaaaaaa")]
    [InlineData("aaaaaaaA")]
    [InlineData("aaaaaaA1")]
    [InlineData("@1aaaaaa")]
    public async Task RegisterUser_InvalidPassword_Error(string password)
    {
        //Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();
        request.Password = password;

        //Act
        var act = async() => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(exception => exception.GetErrors().Count == 1);
    }
    
    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = AutoMappingBuilder.Build();
        var unityOfWork = UnitOfWorkBuilder.Build();
        var writeOnlyRepository = UserRepositoryWriteOnlyBuilder.Build();
        var passwordEncrypter = new PasswordEncrypterBuilder().Build();
        var readOnlyRepository = new UserRepositoryReadOnlyBuilder();

        if (string.IsNullOrWhiteSpace(email) is false)
        {
            readOnlyRepository.ExistsActiveUserWithEmail(email);
        }
        
        return new RegisterUserUseCase(mapper, unityOfWork, writeOnlyRepository, readOnlyRepository.Build(), passwordEncrypter);
    }
}

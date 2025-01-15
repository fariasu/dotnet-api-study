using CommonTestUtilities.Requests;
using FluentAssertions;
using TaskManager.Application.Validators.Users;

namespace Validators.Tests.Users.Login;

public class LoginUserValidatorTest
{
    [Fact]
    public void LoginUser_ValidData_Success()
    {
        //Arrange
        var validator = new LoginUserValidator();
        var request = RequestLoginUserJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void LoginUser_InvalidMail_Error(string mail)
    {
        //Arrange
        var validator = new LoginUserValidator();
        var request = RequestLoginUserJsonBuilder.Build();
        request.Email = mail;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("Email"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaaaaaa")]
    [InlineData("aaaaaaaA")]
    [InlineData("aaaaaaA1")]
    [InlineData("@1aaaaaa")]
    public void LoginUser_InvalidPassword_Error(string password)
    {
        //Arrange
        var validator = new LoginUserValidator();
        var request = RequestLoginUserJsonBuilder.Build();
        request.Password = password;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("password"));
    }
}
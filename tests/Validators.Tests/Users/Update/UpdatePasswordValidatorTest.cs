using CommonTestUtilities.Users.Requests;
using FluentAssertions;
using TaskManager.Application.Validators.Users;

namespace Validators.Tests.Users.Update;

public class UpdatePasswordValidatorTest
{
    [Fact]
    public void UpdatePassword_ValidData_Success()
    {
        //Arrange
        var validator = new UpdatePasswordValidator();
        var request = RequestUpdatePasswordJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("aaaaaaaa")]
    [InlineData("aaaaaaaA")]
    [InlineData("aaaaaaA1")]
    [InlineData("@1aaaaaa")]
    public void UpdatePassword_InvalidPassword_Error(string password)
    {
        //Arrange
        var validator = new UpdatePasswordValidator();
        var request = RequestUpdatePasswordJsonBuilder.Build();
        request.Password = password;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("password"));
    }
}
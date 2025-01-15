using CommonTestUtilities.Users.Requests;
using FluentAssertions;
using TaskManager.Application.Validators.Users;

namespace Validators.Tests.Users.Update;

public class UpdateProfileValidatorTest
{
    [Fact]
    public void UpdateProfile_ValidData_Success()
    {
        //Arrange
        var validator = new UpdateProfileValidator();
        var request = RequestUpdateProfileJsonBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void UpdateProfile_InvalidName_Error(string name)
    {
        //Arrange
        var validator = new UpdateProfileValidator();
        var request = RequestUpdateProfileJsonBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("Name"));
    }
}
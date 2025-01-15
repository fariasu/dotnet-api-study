using CommonTestUtilities.Tasks.Requests;
using FluentAssertions;
using TaskManager.Application.Validators.Tasks;
using TaskManager.Domain.Enums;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace Validators.Tests.Tasks.Create;

public class TaskValidatorTest
{
    [Fact]
    public void CreateTask_ValidData_Success()
    {
        //Arrange
        var validator = new TaskValidator();
        var request = RequestRegisterTaskJsonBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateTask_InvalidTitle_Error(string title)
    {
        //Arrange
        var validator = new TaskValidator();
        var request = RequestRegisterTaskJsonBuilder.Build();
        request.Title = title;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("Title"));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateTask_InvalidDescription_Error(string description)
    {
        //Arrange
        var validator = new TaskValidator();
        var request = RequestRegisterTaskJsonBuilder.Build();
        request.Description = description;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("Description"));
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(99)]
    public void CreateTask_InvalidTaskPriority_Error(int taskPriority)
    {
        //Arrange
        var validator = new TaskValidator();
        var request = RequestRegisterTaskJsonBuilder.Build();
        request.TaskPriority = (TaskPriority)taskPriority;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("Priority"));
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(99)]
    public void CreateTask_InvalidTaskStatus_Error(int taskStatus)
    {
        //Arrange
        var validator = new TaskValidator();
        var request = RequestRegisterTaskJsonBuilder.Build();
        request.TaskStatus = (TaskStatus)taskStatus;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("Status"));
    }
    
    [Theory]
    [InlineData("0001-01-01T00:00:00")]
    [InlineData("1900-01-01T00:00:00")]
    public void CreateTask_InvalidEndDate_Error(string dateString)
    {
        //Arrange
        var validator = new TaskValidator();
        var request = RequestRegisterTaskJsonBuilder.Build();
        request.EndDate = DateTime.Parse(dateString);
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("Date"));
    }
}
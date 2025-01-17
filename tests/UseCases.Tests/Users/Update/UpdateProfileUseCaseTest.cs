using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Services;
using CommonTestUtilities.Users.Requests;
using FluentAssertions;
using TaskManager.Application.UseCases.Users.Update.Profile;
using TaskManager.Domain.Entities;
using TaskManager.Exception.ExceptionsBase;

namespace UseCases.Tests.Users.Update;

public class UpdateProfileUseCaseTest
{
    [Fact]
    public async Task UpdateProfile_ValidData_Success()
    {
        //Arrange
        var request = RequestUpdateProfileJsonBuilder.Build();
        var userEntity = new UserEntity()
        {
            Name = request.Name,
            Email = "test@test.com",
        };
        var useCase = CreateUseCase(userEntity);
        
        //Act
        var act = () => useCase.Execute(request);
        
        //Assert
        await act.Should().NotThrowAsync();
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public async Task UpdateProfile_InvalidName_Error(string name)
    {
        //Arrange
        var request = RequestUpdateProfileJsonBuilder.Build();
        request.Name = name;
        var userEntity = new UserEntity()
        {
            Name = name,
            Email = "test@test.com",
        };
        var useCase = CreateUseCase(userEntity);
        
        //Act
        var act = () => useCase.Execute(request);
        
        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(error => error.GetErrors().Count == 1);
    }
    
    private UpdateProfileUseCase CreateUseCase(UserEntity? userEntity = null)
    {
        var mapper = AutoMappingBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeOnlyRepository = UserRepositoryWriteOnlyBuilder.Build();
        var readOnlyRepository = new UserRepositoryReadOnlyBuilder();
        var loggedUserService = new LoggedUserServiceBuilder();
        
        if (userEntity is not null)
        {
            readOnlyRepository.GetActiveUserWithEmail(userEntity);
            loggedUserService.User(userEntity);
        }
        
        return new UpdateProfileUseCase(mapper, unitOfWork, readOnlyRepository.Build(), loggedUserService.Build());
    }
}
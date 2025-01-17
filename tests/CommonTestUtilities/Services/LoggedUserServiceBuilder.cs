using Moq;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Services;

namespace CommonTestUtilities.Services;

public class LoggedUserServiceBuilder
{
    private readonly Mock<ILoggedUserService> _mock;

    public LoggedUserServiceBuilder()
    {
        _mock = new Mock<ILoggedUserService>();
    }

    public void User(UserEntity userEntity)
    {
        _mock.Setup(loggedUserService => loggedUserService.GetUserAsync()).ReturnsAsync(userEntity);
    }
    
    public ILoggedUserService Build() => _mock.Object;
}
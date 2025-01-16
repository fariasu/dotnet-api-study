using Moq;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Users;

namespace CommonTestUtilities.Repositories;

public class UserRepositoryReadOnlyBuilder
{
    private readonly Mock<IUserRepositoryReadOnly> _mock;

    public UserRepositoryReadOnlyBuilder()
    {
        _mock = new Mock<IUserRepositoryReadOnly>();
    }

    public void ExistsActiveUserWithEmail(string email)
    {
        _mock.Setup(userRepositoryReadOnly => userRepositoryReadOnly
                .ExistsActiveUserWithEmail(email))
            .ReturnsAsync(true);
    }
    
    public void GetActiveUserWithEmail(UserEntity user)
    {
        _mock.Setup(userReadOnlyRepository => userReadOnlyRepository.GetActiveUserWithEmail(user.Email)).ReturnsAsync(user);
    }
    
    public IUserRepositoryReadOnly Build() => _mock.Object;
}
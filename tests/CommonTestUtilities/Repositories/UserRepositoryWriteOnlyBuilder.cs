using Moq;
using TaskManager.Domain.Repositories.Users;

namespace CommonTestUtilities.Repositories;

public static class UserRepositoryWriteOnlyBuilder
{
    public static IUserRepositoryWriteOnly Build() => Mock.Of<IUserRepositoryWriteOnly>();
}
using Moq;
using TaskManager.Domain.Repositories.Db;

namespace CommonTestUtilities.Repositories;

public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build() => Mock.Of<IUnitOfWork>();
}
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.Users;

public interface IUserRepositoryWriteOnly
{
    public Task CreateUser(UserEntity userEntity);
}
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.Users;

public interface IUserRepositoryReadOnly
{
    public Task<bool> ExistsActiveUserWithEmail(string email);
    
    public Task<UserEntity?> GetActiveUserWithEmail(string email);
}
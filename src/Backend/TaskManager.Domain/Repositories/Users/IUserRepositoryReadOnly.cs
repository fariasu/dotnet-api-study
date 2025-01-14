namespace TaskManager.Domain.Repositories.Users;

public interface IUserRepositoryReadOnly
{
    public Task<bool> ExistsActiveUserWithEmail(string email);
}
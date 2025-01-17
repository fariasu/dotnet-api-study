using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Services;

public interface ILoggedUserService
{
    public Task<UserEntity> GetUserAsync();
}
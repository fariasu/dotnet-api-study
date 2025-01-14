using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Users;
using TaskManager.Infrastructure.DataAccess.Db;

namespace TaskManager.Infrastructure.DataAccess.Repositories.Users;

public class UserRepository : IUserRepositoryReadOnly, IUserRepositoryWriteOnly, IUserRepositoryUpdateOnly
{
    private readonly TaskManagerDbContext _dbContext;

    public UserRepository(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateUser(UserEntity userEntity)
    {
        await _dbContext.Users.AddAsync(userEntity);
    }

    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().AnyAsync(user => user.Email == email);
    }
}
using TaskManager.Domain.Repositories.Db;

namespace TaskManager.Infrastructure.DataAccess.Db;

public class UnitOfWork : IUnitOfWork
{
    private readonly TaskManagerDbContext _dbContext;

    public UnitOfWork(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
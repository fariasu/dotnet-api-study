using TaskManager.Domain.Repositories.Task;
using TaskManager.Infrastructure.DataAccess.Db;

namespace TaskManager.Infrastructure.DataAccess.Repositories.Task;

public class TaskRepositoryReadOnly : ITaskRepositoryReadOnly
{
    private readonly TaskManagerDbContext _dbContext;
    
    public TaskRepositoryReadOnly(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
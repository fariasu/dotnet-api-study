using TaskManager.Infrastructure.DataAccess.Db;

namespace TaskManager.Infrastructure.DataAccess.Repositories.Task;

public class TaskRepositoryUpdateOnly
{
    private readonly TaskManagerDbContext _dbContext;
    
    public TaskRepositoryUpdateOnly(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
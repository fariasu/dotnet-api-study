using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Task;
using TaskManager.Infrastructure.DataAccess.Db;

namespace TaskManager.Infrastructure.DataAccess.Repositories.Task;

public class TaskRepositoryWriteOnly : ITaskRepositoryWriteOnly
{
    private readonly TaskManagerDbContext _dbContext;
    
    public TaskRepositoryWriteOnly(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async System.Threading.Tasks.Task CreateTask(TaskEntity taskEntity)
    {
        await _dbContext.AddAsync(taskEntity);
    }
}
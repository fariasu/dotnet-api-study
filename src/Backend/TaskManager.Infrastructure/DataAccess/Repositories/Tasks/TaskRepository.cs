using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.Tasks;
using TaskManager.Infrastructure.DataAccess.Db;

namespace TaskManager.Infrastructure.DataAccess.Repositories.Tasks;

public class TaskRepository : ITaskRepositoryWriteOnly, ITaskRepositoryReadOnly, ITaskRepositoryUpdateOnly
{
    private readonly TaskManagerDbContext _dbContext;
    
    public TaskRepository(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateTask(TaskEntity taskEntity)
    {
        await _dbContext.AddAsync(taskEntity);
    }

    public void DeleteTask(TaskEntity taskEntity)
    {
        _dbContext.Tasks.Remove(taskEntity);
    }

    public async Task<List<TaskEntity>?> GetAll(long creatorId, int page, int pageSize)
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .Where(task => task.CreatorId == creatorId)
            .Skip((page -1 ) * pageSize)
            .Take(pageSize)
            .OrderByDescending(task => task.TaskPriority)
            .ToListAsync();
    }

    public async Task<TaskEntity?> GetByIdNoTracking(long id, long creatorId)
    {
        return await _dbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(task => task.Id == id && task.CreatorId == creatorId);
    }

    public async Task<TaskEntity?> GetById(long id, long creatorId)
    {
        return await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == id && task.CreatorId == creatorId);
    }

    public void Update(TaskEntity task)
    {
         _dbContext.Tasks.Update(task);
    }
}
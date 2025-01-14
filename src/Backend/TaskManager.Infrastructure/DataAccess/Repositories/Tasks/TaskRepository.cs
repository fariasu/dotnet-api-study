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

    public async Task<List<TaskEntity>> GetAll()
    {
        return await _dbContext.Tasks.AsNoTracking().ToListAsync();
    }

    public async Task<TaskEntity?> GetByIdNoTracking(long id)
    {
        return await _dbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TaskEntity?> GetById(long id)
    {
        return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(TaskEntity task)
    {
         _dbContext.Tasks.Update(task);
    }
}
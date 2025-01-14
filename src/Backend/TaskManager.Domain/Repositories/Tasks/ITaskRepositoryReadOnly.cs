using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.Tasks;

public interface ITaskRepositoryReadOnly
{
    public Task<List<TaskEntity>> GetAll();
    
    public Task<TaskEntity?> GetByIdNoTracking(long id);
    public Task<TaskEntity?> GetById(long id);
}
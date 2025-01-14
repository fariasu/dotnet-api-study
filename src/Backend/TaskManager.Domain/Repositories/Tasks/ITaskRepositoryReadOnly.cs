using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.Tasks;

public interface ITaskRepositoryReadOnly
{
    public Task<List<TaskEntity>?> GetAll(long creatorId);
    public Task<TaskEntity?> GetByIdNoTracking(long id, long creatorId);
    public Task<TaskEntity?> GetById(long id, long creatorId);
}
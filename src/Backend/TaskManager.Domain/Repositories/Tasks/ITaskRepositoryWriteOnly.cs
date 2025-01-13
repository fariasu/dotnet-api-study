using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.Tasks;

public interface ITaskRepositoryWriteOnly
{
    public Task CreateTask(TaskEntity taskEntity);
    public void DeleteTask(TaskEntity taskEntity);
}
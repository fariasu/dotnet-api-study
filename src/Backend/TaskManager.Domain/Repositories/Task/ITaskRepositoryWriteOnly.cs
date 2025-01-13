using TaskManager.Domain.Entities;
using System.Threading.Tasks;

namespace TaskManager.Domain.Repositories.Task;

public interface ITaskRepositoryWriteOnly
{
    public System.Threading.Tasks.Task CreateTask(TaskEntity taskEntity);
}
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.Tasks;

public interface ITaskRepositoryUpdateOnly
{
    public void Update(TaskEntity task);
}
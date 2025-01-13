namespace TaskManager.Domain.Repositories.Db;

public interface IUnitOfWork
{
    public Task CommitAsync();
}
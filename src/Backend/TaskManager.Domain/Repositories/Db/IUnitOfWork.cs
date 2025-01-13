namespace TaskManager.Domain.Repositories.Db;

public interface IUnitOfWork
{
    public System.Threading.Tasks.Task Commit();
}
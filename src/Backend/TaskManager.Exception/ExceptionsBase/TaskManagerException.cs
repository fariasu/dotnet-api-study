namespace TaskManager.Exception.ExceptionsBase;

public abstract class TaskManagerException(string message) : SystemException(message)
{
    public abstract int StatusCode { get; }

    public abstract List<string> GetErrors();
}
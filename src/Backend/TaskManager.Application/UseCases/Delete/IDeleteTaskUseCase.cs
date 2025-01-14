namespace TaskManager.Application.UseCases.Delete;

public interface IDeleteTaskUseCase
{
    public Task Execute(long id);
}
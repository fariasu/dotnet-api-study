namespace TaskManager.Application.UseCases.Delete;

public interface IDeleteTaskUseCase
{
    public Task Execute(int id);
}
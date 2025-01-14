namespace TaskManager.Communication.DTOs.Tasks.Response;

public record ResponseCreatedTaskJson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
namespace TaskManager.Communication.DTOs.Response;

public record ResponseCreatedTaskJson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
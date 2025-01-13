using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Communication.DTOs.Response;

public class ResponseTaskShortJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EndDate { get; set; }
    public TaskStatus TaskStatus { get; set; } = TaskStatus.Pending;
}
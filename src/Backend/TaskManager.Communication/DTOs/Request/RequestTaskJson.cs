using TaskManager.Domain.Enums;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Communication.DTOs.Request;

public record RequestTaskJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EndDate { get; set; }
    public TaskPriority TaskPriority { get; set; } = TaskPriority.Normal;
    public TaskStatus TaskStatus { get; set; } = TaskStatus.Pending;
}
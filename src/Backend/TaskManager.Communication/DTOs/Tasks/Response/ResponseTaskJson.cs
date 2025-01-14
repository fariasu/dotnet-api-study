using TaskManager.Domain.Enums;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Communication.DTOs.Tasks.Response;

public class ResponseTaskJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EndDate { get; set; }
    public TaskPriority TaskPriority { get; set; } = TaskPriority.Normal;
    public TaskStatus TaskStatus { get; set; } = TaskStatus.Pending;
}
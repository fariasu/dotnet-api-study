using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Enums;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Communication.DTOs.Request;

public record RequestTaskJson
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public TaskPriority TaskPriority { get; set; } = TaskPriority.Normal;

    [Required]
    public TaskStatus TaskStatus { get; set; } = TaskStatus.Pending;
}
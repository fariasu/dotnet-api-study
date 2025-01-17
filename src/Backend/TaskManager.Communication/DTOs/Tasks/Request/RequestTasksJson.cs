using System.ComponentModel.DataAnnotations;

namespace TaskManager.Communication.DTOs.Tasks.Request;

public class RequestTasksJson
{
    [Required] public int Page { get; set; }

    [Required] public int PageSize { get; set; }
}
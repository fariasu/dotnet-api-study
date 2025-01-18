using System.ComponentModel.DataAnnotations;

namespace TaskManager.Communication.DTOs.Tasks.Request;

public class RequestTasksJson
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}
namespace TaskManager.Communication.DTOs.Tasks.Response;

public class ResponseTasksJson
{
    public List<ResponseTaskShortJson> Tasks { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
}
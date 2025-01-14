using System.ComponentModel.DataAnnotations;

namespace TaskManager.Communication.DTOs.Users.Requests;

public class RequestUpdateProfileJson
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
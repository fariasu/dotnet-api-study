using System.ComponentModel.DataAnnotations;

namespace TaskManager.Communication.DTOs.Users.Requests;

public class RequestUpdatePasswordJson
{
    [Required]
    public string Password { get; set; } = string.Empty;
}
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Communication.DTOs.Users.Requests;

public class RequestLoginUserJson
{
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}
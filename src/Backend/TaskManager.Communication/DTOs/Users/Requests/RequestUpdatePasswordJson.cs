using System.ComponentModel.DataAnnotations;

namespace TaskManager.Communication.DTOs.Users.Requests;

public class RequestUpdatePasswordJson
{
    public string Password { get; set; } = string.Empty;
}
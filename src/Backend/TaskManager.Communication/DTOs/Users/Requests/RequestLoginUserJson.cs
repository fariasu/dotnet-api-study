﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.Communication.DTOs.Users.Requests;

public class RequestLoginUserJson
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
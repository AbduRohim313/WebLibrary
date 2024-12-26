﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class UserSettingsDto
{
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}
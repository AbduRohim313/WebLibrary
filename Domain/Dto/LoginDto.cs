﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.UserDto;

public class LoginDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
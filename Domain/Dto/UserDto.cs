﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class UserDto
{
    public string Id { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
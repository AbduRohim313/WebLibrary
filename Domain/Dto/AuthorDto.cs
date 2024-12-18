﻿using System.ComponentModel.DataAnnotations;
using Domain.Entity;

namespace Domain.Dto.UserDto;

public class AuthorDto
{
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; }
}
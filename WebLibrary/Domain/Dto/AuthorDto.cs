using System.ComponentModel.DataAnnotations;
using Domain.Entity;

namespace Domain.Dto;

public class AuthorDto
{
    [Required]
    public string FullName { get; set; }
    
    public Book Book { get; set; }
}
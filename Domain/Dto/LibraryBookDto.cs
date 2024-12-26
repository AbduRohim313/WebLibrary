using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class LibraryBookDto
{
    [Required]
    public string FullName { get; set; }
}
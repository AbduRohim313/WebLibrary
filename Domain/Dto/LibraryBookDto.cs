using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class LibraryBookDto
{
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; }
}
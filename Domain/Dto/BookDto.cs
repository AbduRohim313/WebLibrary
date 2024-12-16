using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class BookDto
{
    public int BookId { get; set; }
    [Required]
    public string Name { get; set; }
}
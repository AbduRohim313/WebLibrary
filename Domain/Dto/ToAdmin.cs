using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class ToAdminDto
{
    [Required]
    public string Name { get; set; }
}
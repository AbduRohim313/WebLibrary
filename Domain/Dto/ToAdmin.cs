using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.UserDto;

public class ToAdminDto
{
    [Required]
    public string Name { get; set; }
}
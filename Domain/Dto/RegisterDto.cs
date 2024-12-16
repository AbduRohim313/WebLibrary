using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Password { get; set; }
}
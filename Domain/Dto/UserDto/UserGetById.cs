using System.ComponentModel.DataAnnotations;
using Domain.Entity;

namespace Domain.Dto.UserDto;

public class UserGetById
{
    public string Id { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }

    public IEnumerable<Book> Books { get; set; }
}
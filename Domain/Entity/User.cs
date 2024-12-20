using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity;

public class User : IdentityUser
{
    // public Position Position { get; set; }
    public IEnumerable<Book> Books { get; set; }
}
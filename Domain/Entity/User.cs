using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity;

public class User : IdentityUser
{
    // public Position Position { get; set; }
    public List<Book> Books { get; set; } = new List<Book>();

    public User()
    {
        Books = new List<Book>();
    }
}
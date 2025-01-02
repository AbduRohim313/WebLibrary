using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity;

public class User : IdentityUser
{
    // public Position Position { get; set; } = Position.User;
    public List<Book> Books { get; set; } = new List<Book>();
    
}
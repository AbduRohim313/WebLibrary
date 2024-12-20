using Domain.Dto.UserDto;

namespace Domain.Entity;

public class Book : BookBase
{
    // public int BookId { get; set; }
    // public string FullName { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    // public List<Author> Authors { get; set; } = new();
}
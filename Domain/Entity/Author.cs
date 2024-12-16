namespace Domain.Entity;

public class Author
{
    public int AuthorId { get; set; }
    public string FullName { get; set; }
    public List<Book> Books { get; set; } = new();
}
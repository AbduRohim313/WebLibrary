namespace Domain.Entity;

public class Author
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public Book Book { get; set; }
}
using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected AppDbContext()
    {
        
    }

    public DbSet<Book> Books { get; set; }
    // public DbSet<Admin> Admins { get; set; }
    public DbSet<LibraryBook> LibraryBooks { get; set; }
    // public DbSet<Author> Authors { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasKey(a => a.Id);
        // Настройка связи "один ко многим" с помощью Fluent API
        modelBuilder.Entity<User>()
            .HasMany(a => a.Books)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<Book>()
            .HasKey(b => b.BookId);
        modelBuilder.Entity<LibraryBook>()
            .HasKey(b => b.BookId);

        // modelBuilder.Entity<Book>()
        //     .HasMany(c => c.Authors)
        //     .WithMany(s => s.Books);
    }
}
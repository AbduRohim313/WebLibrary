using Domain;

namespace WebApi.Service;

using Domain.Entity;
using Microsoft.EntityFrameworkCore;

public class LibraryBookService
{
    private readonly AppDbContext _context;

    public LibraryBookService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddLibraryBookAsync(LibraryBook libraryBook)
    {
        _context.LibraryBooks.Add(libraryBook);
        await _context.SaveChangesAsync();
    }
}
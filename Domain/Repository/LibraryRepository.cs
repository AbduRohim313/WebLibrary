using Domain.Entity;
using Domain.Interface;

namespace Domain.Repository;

public class LibraryRepository : ICreateRepository<LibraryBook>, IRDRepository<LibraryBook>
{
    AppDbContext _context;

    public LibraryRepository(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<IEnumerable<LibraryBook>> GetAll()
    {
        return _context.LibraryBooks.ToArray();
    }

    public async Task<LibraryBook> GetByIdAsync(int id)
    {
        return _context.LibraryBooks.Find(id);
    }

    public async Task<LibraryBook> Add(LibraryBook entity)
    {
        _context.LibraryBooks.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        _context.LibraryBooks.Remove(_context.LibraryBooks.Find(id));
        await _context.SaveChangesAsync();
        return true;

    }
}
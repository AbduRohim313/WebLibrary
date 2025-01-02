using Domain.Entity;
using Domain.Interface;

namespace Domain.Repository;

public class LibraryRepository : IAddRepository<LibraryBook>, IGetRemoveRepository<LibraryBook>
{
    AppDbContext _context;

    public LibraryRepository(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<IEnumerable<LibraryBook>> GetAllAsync()
    {
        return _context.LibraryBooks.ToArray();
    }

    public async Task<LibraryBook> GetByIdAsync(int id)
    {
        return _context.LibraryBooks.Find(id);
    }

    public async Task<LibraryBook> AddAsync(LibraryBook entity)
    {
        _context.LibraryBooks.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        _context.LibraryBooks.Remove(_context.LibraryBooks.Find(id)!);
        await _context.SaveChangesAsync();
        return true;

    }
}
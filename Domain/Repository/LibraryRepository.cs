using Domain.Entity;
using Domain.Interface;

namespace Domain.Repository;

public class LibraryRepository : IRepository<LibraryBook>
{
    AppDbContext context;

    public LibraryRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<LibraryBook>> GetAll()
    {
        return context.LibraryBooks.ToArray();
    }

    public async Task<LibraryBook> GetByIdAsync(int id)
    {
        return context.LibraryBooks.Find(id);
    }

    public async Task<LibraryBook> Add(LibraryBook entity)
    {
        throw new NotImplementedException();
    }

    public async Task<LibraryBook> Update(LibraryBook entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(int id)
    {
        context.LibraryBooks.Remove(context.LibraryBooks.Find(id));
        return context.SaveChanges() > 0;
    }
}
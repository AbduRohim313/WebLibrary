using Domain.Entity;
using Domain.Interface;

namespace Domain.Repository;

public class BookRepository : IRepository<Book>, IRemoveByUser
{
    private AppDbContext _dbContext;

    public BookRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Book>> GetAll()
    {
        return _dbContext.Books.ToList();
    }

    public async Task<Book> GetByIdAsync(int id) => (await _dbContext.Books.FindAsync(id))!;

    public async Task<Book> Add(Book entity)
    {
        _dbContext.Books.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Book> Update(Book entity)
    {
        _dbContext.Books.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        _dbContext.Books.Remove((await _dbContext.Books.FindAsync(id))!);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveByUser(User user, int id)
    {
        // _dbContext.Books.Remove((await _dbContext.Books.FindAsync(id))!);
        user.Books.Remove(await GetByIdAsync(id));
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<Book> Toplam(string userId)
    {
        throw new NotImplementedException();
    }
}
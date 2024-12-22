using Domain.Entity;
using Domain.Interface;

namespace Domain.Repository;

public class BookRepository : IRepository<Book>
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

    public async Task<Book> GetById(int id)
    {
        return await _dbContext.Books.FindAsync(id);
    }

    public async Task<Book> Add(Book entity)
    {
        _dbContext.Books.Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<Book> Update(Book entity)
    {
        _dbContext.Books.Update(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        _dbContext.Books.Remove((await _dbContext.Books.FindAsync(id))!);
        _dbContext.SaveChanges();
        return true;
    }
}
using Domain.Entity;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository;

public class AuthorRepository : IRepository<Author>
{
    AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAll()
    {
        return await _context.Authors.ToArrayAsync();
    }

    public async Task<Author> GetById(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task<Author> Add(Author entity)
    {
        _context.Authors.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Author> Update(Author entity)
    {
        var author = _context.Authors.Find(entity.AuthorId);
        author.FullName = entity.FullName;
        _context.Authors.Update(author);

        await _context.SaveChangesAsync();
        return author;
    }

    public async Task<bool> Delete(int id)
    {
        _context.Authors.Remove(_context.Authors.Find(id));

        await _context.SaveChangesAsync();
        return true;
    }
}
using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Interface;

namespace Domain.Repository;

public class UserRepository// : IUserRepository
{
    AppDbContext context;

    public UserRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<bool> RemoveBookFromUser(Book book)
    {
        context.Books.Remove(book);
        await context.SaveChangesAsync();
        return true;
    }
}
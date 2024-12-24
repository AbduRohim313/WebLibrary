using Domain.Entity;

namespace Domain.Interface;

public interface IRemoveByUser
{
    public Task<bool> RemoveByUser(User user, int id);
    public Task<Book> Toplam(string userId);
    
}
using Domain.Entity;

namespace Domain.Interface;

public interface IRemoveByUser
{
    public Task<bool> RemoveByUser(User user, int id);
}
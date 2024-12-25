namespace Domain.Interface;

public interface IAdminRepository<T>
{
    public Task<T> GetByIdAsync(string id);
    public Task<T> CheckPassowrd(string password);
}
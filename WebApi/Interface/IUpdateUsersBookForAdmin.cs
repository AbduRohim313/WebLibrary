namespace WebApi.Interface;

public interface IUpdateUsersBookForAdmin<T>
{
    public Task<T> Create(string userId, T dto);
    public Task<bool> Delete(int bookId);
}
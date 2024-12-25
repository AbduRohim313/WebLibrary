namespace WebApi.Interface;

public interface IUpdateUsersBookForAdmin<T>
{
    public Task<T> Create(T dto);
    public Task<bool> Delete(int id);
}
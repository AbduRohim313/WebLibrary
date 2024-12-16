namespace WebApi.Interface;

public interface IService<T>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T>? GetById(int id);
    public Task<T> Create(T data);
    public Task<T> Update(T data);
    public Task<bool> Delete(int id);
}
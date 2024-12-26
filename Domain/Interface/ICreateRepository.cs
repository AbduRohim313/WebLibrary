namespace Domain.Interface;

public interface ICreateRepository<T>
{
    Task<T> Add(T entity);
}
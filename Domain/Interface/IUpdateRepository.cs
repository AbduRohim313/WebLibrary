namespace Domain.Interface;

public interface IUpdateRepository<T>
{
    Task<T> UpdateAsync(T entity);
}
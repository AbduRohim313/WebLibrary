namespace Domain.Interface;

public interface IUpdateRepository<T>
{
    Task<T> Update(T entity);
}
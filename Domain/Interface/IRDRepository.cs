namespace Domain.Interface;

public interface IRDRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetByIdAsync(int id);
    Task<bool> Delete(int id);
}
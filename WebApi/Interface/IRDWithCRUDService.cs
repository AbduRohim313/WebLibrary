namespace WebApi.Interface;

public interface IRDWithCRUDService<T, K> // mawuni qib boldim togirlaw kk endi qoganini
{
    public Task<IEnumerable<T>> ReadAllAsync();
    public Task<T> ReadByIdAsync(K id);
    public Task<bool> DeleteAsync(K id);
}
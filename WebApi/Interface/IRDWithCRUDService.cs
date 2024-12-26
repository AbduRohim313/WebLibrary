namespace WebApi.Interface;

public interface IRDWithCRUDService<T, K> // mawuni qib boldim togirlaw kk endi qoganini
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(K id);
    public Task<bool> Delete(K id);
}
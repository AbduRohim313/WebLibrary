namespace WebApi.Interface;

public interface IRDWithCRUD<T> // mawuni qib boldim togirlaw kk endi qoganini
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(string id);
    public Task<bool> Delete(string id);
}
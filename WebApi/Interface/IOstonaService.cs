namespace WebApi.Interface;

public interface IOstonaService<T>
{
    public Task<T> KitobObKetish(int id);
    public Task<T> KitobQaytarish(int id);
    public Task<IEnumerable<T>> Toplam();
}
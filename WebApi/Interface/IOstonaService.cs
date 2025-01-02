namespace WebApi.Interface;

public interface IOstonaService<T>
{
    public Task<T> KitobObKetishAsync(int id);
    public Task<T> KitobQaytarishAsync(int id);
    public Task<IEnumerable<T>> ToplamAsync();
}
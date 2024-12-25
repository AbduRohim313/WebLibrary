namespace WebApi.Interface;

public interface IAdminService<T>
{
    public Task<T> GetAdmin(string id);
    public Task<T> GetPassword(string id);
}
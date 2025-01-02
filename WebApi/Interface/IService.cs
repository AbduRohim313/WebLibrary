using Domain.Dto.UserDto;
using Domain.Entity;

namespace WebApi.Interface;

public interface IService<T>
{
    public Task<IEnumerable<T>> ReadAllAsync();
    public Task<T> ReadByIdAsync(int id);
    public Task<T> CreateAsync(T data);
    public Task<T> UpdateAsync(T data);
    public Task<bool> DeleteAsync(int id);
}
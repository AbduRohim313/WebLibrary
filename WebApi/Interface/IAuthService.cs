using Domain.Dto.UserDto;

namespace WebApi.Interface;

public interface IAuthService<T, K>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<K> GetById(string id);
    public Task<ResponceDto> Create(LoginDto data);
    public Task<ResponceDto> Update(T data);
    public Task<bool> Delete(string id);
}
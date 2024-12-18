using Domain.Dto;

namespace WebApi.Interface;

public interface IAuthService<T>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(string id);
    public Task<ResponceDto> Create(LoginDto data);
    public Task<ResponceDto> Update(T data);
    public Task<bool> Delete(string id);
}
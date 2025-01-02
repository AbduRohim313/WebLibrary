using Domain.Dto.UserDto;

namespace WebApi.Interface;

public interface ICreateService<T>
{
    public Task<ResponceDto> CreateAsync(T dto);
}
using Domain.Dto.UserDto;

namespace WebApi.Interface;

public interface ICreate<T>
{
    public Task<ResponceDto> Create(T dto);
}
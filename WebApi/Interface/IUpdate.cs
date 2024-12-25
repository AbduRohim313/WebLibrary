using Domain.Dto.UserDto;

namespace WebApi.Interface;

public interface IUpdate<T>
{
    public Task<ResponceDto> Update(T dto);
}
using Domain.Dto.UserDto;

namespace WebApi.Interface;

public interface IUpdateService<T>
{
    public Task<ResponceDto> UpdateAsync(T dto);
}
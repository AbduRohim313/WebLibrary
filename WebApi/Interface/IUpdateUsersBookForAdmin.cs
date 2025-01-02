using Domain.Dto.UserDto;

namespace WebApi.Interface;

public interface IUpdateUsersBookForAdmin<T>
{
    public Task<ResponceDto> CreateAsync(string userId, T dto);
    public Task<bool> DeleteAsync(int bookId);
}
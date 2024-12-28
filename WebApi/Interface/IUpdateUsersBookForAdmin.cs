using Domain.Dto.UserDto;

namespace WebApi.Interface;

public interface IUpdateUsersBookForAdmin<T>
{
    public Task<ResponceDto> Create(string userId, T dto);
    public Task<bool> Delete(int bookId);
}
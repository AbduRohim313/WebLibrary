using Domain.Dto;

namespace WebApi.Interface;

public interface IUserSettingsService<T>
{
    public Task<T> UpdateUsersSettingsAsync(UserSettingsDto dto);
}
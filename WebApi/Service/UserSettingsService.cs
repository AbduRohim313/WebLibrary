using Domain.Dto;
using Domain.Dto.UserDto;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Service;

public class UserSettingsService : IUserSettingsService<ResponceDto>
{
    UserManager<User> _userManager;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserSettingsService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    private string GetUserIdFromToken()
    {
        var userClaim =
            _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        return userClaim?.Value ?? string.Empty;
    }

    public async Task<ResponceDto> UpdateUsersSettings(UserSettingsDto dto)
    {
        var user = await _userManager.FindByIdAsync(GetUserIdFromToken());
        if (user == null)
            return null;
        user.UserName = dto.UserName;
        user.PhoneNumber = dto.PhoneNumber;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return new ResponceDto()
            {
                Status = "error",
                Message = "user ozgartirilmadi",
            };
        var removePasswordResult = await _userManager.RemovePasswordAsync(user);
        if (!removePasswordResult.Succeeded)
        {
            return new ResponceDto
            {
                Status = "error",
                Message = "Parolni o'chirishda xatolik: " +
                          string.Join(", ", removePasswordResult.Errors.Select(e => e.Description))
            };
        }

        // Установить новый пароль
        var addPasswordResult = await _userManager.AddPasswordAsync(user, dto.Password);
        if (!addPasswordResult.Succeeded)
        {
            return new ResponceDto
            {
                Status = "error",
                Message = "Yangi parolni o'rnatishda xatolik: " +
                          string.Join(", ", addPasswordResult.Errors.Select(e => e.Description))
            };
        }

        return new ResponceDto() { Status = "Success", Message = "User mufiaqatli ozgartirildi" };
    }
}
using System.Security.Claims;
using Domain.Dto.UserDto;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using WebApi.Interface;

namespace WebApi.Service;

public class UserSettingsService : IUserSettingsService<UserDto>
{
    
    UserManager<User> _userManager;
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserSettingsService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<UserDto> ChangeUserName()
    {
        var user = await GetUserByClaimAsync();
        return new UserDto();
    }

    public async Task<UserDto> ChangePassword()
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> ChangePhone()
    {
        throw new NotImplementedException();
    }
    
    
    private async Task<User> GetUserByClaimAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            throw new Exception("HttpContext is null!");

        var user = httpContext.User;
        if (user == null)
            throw new Exception("User is null!");
        var userIdClaim = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            throw new Exception("User ID not found in token!");

        var userId = userIdClaim.Value;
        // Logic to add book to user and remove from library
        var userEntity = _userManager.Users.FirstOrDefault(x => x.Id == userId);
        // var userEntity = _userManager.Users
        //     .Include(u => u.Books) // Подключение навигационного свойства
        //     .FirstOrDefault(x => x.Id == userId);
        if (userEntity == null)
            throw new Exception("User not found!");
        return userEntity;
    }
}
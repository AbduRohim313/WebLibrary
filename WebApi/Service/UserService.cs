using Domain.Dto;
using Domain.Entity;
using Domain.Enums;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Service;

// [Route("[controller]")]
// [ApiController]
public class UserService : IAuthService<UserDto>
{
    UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        var allUsers = _userManager.Users;
        var result = new List<UserDto>();
        foreach (var user in allUsers)
        {
            result.Add(new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
            });
        }

        return result;
    }

    public async Task<UserDto> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
            return new UserDto()
            {
                UserName = user.UserName!,
            };
        return null;
    }

    public async Task<ResponceDto> Create(LoginDto dto)
    {
        
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user != null)
            return null;

        var newUser = new User()
        {
            UserName = dto.UserName,
            PasswordHash = dto.Password,
        };

        var result = await _userManager.CreateAsync(newUser, dto.Password);
        if (!result.Succeeded)
        {
            return new ResponceDto()
            {
                Status = "error",
                Message = "user yaratilmadi",
            };
        }
        await _userManager.AddToRoleAsync(newUser, Position.User.ToString());

        return new ResponceDto() { Status = "Success", Message = "User mufiaqatli yaratildi" };
    }

    public async Task<ResponceDto> Update(UserDto userDto)
    {
        var user = await _userManager.FindByNameAsync(userDto.UserName);
        if(user == null)
            return null;
        user.UserName = userDto.UserName;
        user.PasswordHash = userDto.Password;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return new ResponceDto()
            {
                Status = "error",
                Message = "user yaratilmadi",
            };
        return new ResponceDto(){Status = "Success", Message = "User mufiaqatli ozgartirildi"};

    }


    public async Task<bool> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
            return true;
        }
        return false;
    }
}
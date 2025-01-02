using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Enums;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Service;

public class UserService : IRDWithCRUDService<UserDto, string>, IUpdateService<UserDto>
{
    UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserDto>> ReadAllAsync()
    {
        var allUsers = _userManager.Users;
        return allUsers.Select(x => new UserDto()
        {
            Id = x.Id,
            UserName = x.UserName,
        });
    }

    public async Task<UserDto> ReadByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName!,
            };
        return null;
    }

    public async Task<ResponceDto> UpdateAsync(UserDto userDto)
    {
        var user = await _userManager.FindByIdAsync(userDto.Id);
        if (user == null)
            return null;

        user.UserName = userDto.UserName;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return new ResponceDto()
            {
                Status = "error",
                Message = "user o'zgartirilmadi",
            };

        // Создаем экземпляр PasswordValidator
        var passwordValidator = new PasswordValidator<User>();
        var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, user, userDto.Password);


        if (!passwordValidationResult.Succeeded)
        {
            return new ResponceDto
            {
                Status = "error",
                Message = "Пароль не соответствует требованиям: " +
                          string.Join(", ", passwordValidationResult.Errors.Select(e => e.Description))
            };
        }

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


        var addPasswordResult = await _userManager.AddPasswordAsync(user, userDto.Password);
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

    public async Task<bool> DeleteAsync(string id)
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
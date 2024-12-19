using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Enums;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Service;

// [Route("[controller]")]
// [ApiController]
public class UserService : IAuthService<UserDto, UserGetById>
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

    public async Task<UserGetById> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
            return new UserGetById()
            {
                Id = user.Id,
                UserName = user.UserName!,
                Books = user.Books
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
        var user = await _userManager.FindByIdAsync(userDto.Id);
        if (user == null)
            return null;
        user.UserName = userDto.UserName;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return new ResponceDto()
            {
                Status = "error",
                Message = "user yaratilmadi",
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
    //
    // public async Task<ResponceDto> RemoveBookFromUser(string userId, int bookId)
    // {
    //     try
    //     {
    //         // Найти книгу по её ID и UserId
    //         var book = await _bookService.GetById(bookId);
    //         if (book == null)
    //         {
    //             return new ResponceDto
    //             {
    //                 Status = "error",
    //                 Message = "Книга не найдена у данного пользователя"
    //             };
    //         }
    //
    //         // Удалить книгу
    //         await _bookService.Delete(bookId);
    //
    //         return new ResponceDto
    //         {
    //             Status = "success",
    //             Message = "Книга успешно удалена у пользователя"
    //         };
    //     }
    //     catch (Exception ex)
    //     {
    //         // Логирование ошибки
    //         Console.WriteLine($"Ошибка при удалении книги: {ex.Message}");
    //         return new ResponceDto
    //         {
    //             Status = "error",
    //             Message = "Произошла ошибка при удалении книги"
    //         };
    //     }
    // }

}
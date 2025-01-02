using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[Route("adminPage/[controller]")]
[Authorize(Roles = nameof(Position.Admin))]
public class UpdateUsersBookController : ControllerBase
{
    IUpdateUsersBookForAdmin<BookDto> _updateUsersBookForAdmin;

    public UpdateUsersBookController(IUpdateUsersBookForAdmin<BookDto> updateUsersBookForAdmin)
    {
        _updateUsersBookForAdmin = updateUsersBookForAdmin;
    }

    [HttpPost("{UserId}")]
    public async Task<IActionResult> PostAsync(string UserId, [FromBody] BookDto data)
    {
        var responce = await _updateUsersBookForAdmin.CreateAsync(UserId, data);
        if (responce.Status == "error 404")
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponceDto()
            {
                Message = "Foydalanuvchi topilmadi!",
                Status = "error"
            });
        }

        if (responce.Status == "error 400")
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponceDto()
            {
                Message = "Malumotlar to'ldirilmadi!",
                Status = "error"
            });
        }

        return Ok(responce);
    }

    [HttpDelete("{BookId}")]
    public async Task<IActionResult> DeleteAsync(int BookId)
    {
        var responce = await _updateUsersBookForAdmin.DeleteAsync(BookId);
        if (responce == false)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponceDto()
            {
                Message = "bunday kitob mavjud emas!",
                Status = "error"
            });
        }

        return Ok(new ResponceDto()
        {
            Message = "Kitob o'chirildi!",
            Status = "success"
        });
    }
}
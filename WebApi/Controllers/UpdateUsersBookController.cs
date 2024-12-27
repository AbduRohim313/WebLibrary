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

    [HttpPost("{UseerId}")]
    public async Task<IActionResult> Post(string UseerId, [FromBody] BookDto data)
    {
        var responce = await _updateUsersBookForAdmin.Create(UseerId,data);
        if (responce == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponceDto()
            {
                Message = "qowilmadi!",
                Status = "error"
            });
        }

        return Ok(responce);


        return StatusCode(StatusCodes.Status500InternalServerError);
    }
    [HttpDelete("{BookId}")]
    public async Task<IActionResult> Delete(int BookId)
    {
        var responce = await _updateUsersBookForAdmin.Delete(BookId);
        if (responce == false)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponceDto()
            {
                Message = "o'chirilmadi!",
                Status = "error"
            });
        }

        return Ok(new ResponceDto()
        {
            Message = "o'chirildi!",
            Status = "success"
        });
    }
}
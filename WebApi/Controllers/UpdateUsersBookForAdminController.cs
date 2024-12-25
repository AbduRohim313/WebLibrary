﻿using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[Route("updateUsersBookForAdmin/[controller]")]

[Authorize(Roles = nameof(Position.Admin))]
public class UpdateUsersBookForAdminController : ControllerBase
{
    IUpdateUsersBookForAdmin<BookDto> _updateUsersBookForAdmin;

    public UpdateUsersBookForAdminController(IUpdateUsersBookForAdmin<BookDto> updateUsersBookForAdmin)
    {
        _updateUsersBookForAdmin = updateUsersBookForAdmin;
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Post(string userId, [FromBody] BookDto data)
    {
        var responce = await _updateUsersBookForAdmin.Create(userId,data);
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var responce = await _updateUsersBookForAdmin.Delete(id);
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
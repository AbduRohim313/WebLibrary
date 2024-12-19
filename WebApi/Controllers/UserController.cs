using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[Route("adminPage/[controller]CRUD")]
[ApiController]
[Authorize(Roles = nameof(Position.Admin))]
public class UserController : ControllerBase
{
    IAuthService<UserDto, UserGetById> _userService;

    public UserController(IAuthService<UserDto, UserGetById> userService)
    {
        _userService = userService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _userService.GetById(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post(LoginDto data)
    {
        var responce = await _userService.Create(data);
        if (responce == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponceDto()
            {
                Message = "bunday foydalanuvchi bor!",
                Status = "error"
            });
        }

        if (responce.Status == "success")
        {
            return Ok(responce);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UserDto data)
    {
        var responce = await _userService.Update(data);
        if (responce == null)
            return StatusCode(StatusCodes.Status400BadRequest, new ResponceDto()
            {
                Message = "bunday foydalanuvchi yoq!",
            });
        if (responce.Status == "Success")
        {
            return Ok(responce);
        }
        if (responce.Status == "error")
        {
            BadRequest(responce.Message);
        }
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return await _userService.Delete(id) ? Ok("deleted") : BadRequest();
    }
}
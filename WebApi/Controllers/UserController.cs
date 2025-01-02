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
    private IRDWithCRUDService<UserDto, string> _irdWithCrudService;
    private IUpdateService<UserDto> _updateService;

    public UserController(IUpdateService<UserDto> updateService, IRDWithCRUDService<UserDto, string> irdWithCrudService)
    {
        _updateService = updateService;
        _irdWithCrudService = irdWithCrudService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        return Ok(_irdWithCrudService.ReadAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var user = await _irdWithCrudService.ReadByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(UserDto data)
    {
        var responce = await _updateService.UpdateAsync(data);
        if (responce == null)
            return StatusCode(StatusCodes.Status404NotFound, new ResponceDto()
            {
                Message = "bunday foydalanuvchi yoq!",
            });
        if (responce.Status == "Success")
        {
            return Ok(responce);
        }

        if (responce.Status == "error")
        {
            return BadRequest(responce.Message); // return qop ketibti
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        return await _irdWithCrudService.DeleteAsync(id) ? Ok("deleted") : NotFound();
    }
}
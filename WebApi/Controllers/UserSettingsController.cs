using Domain.Dto;
using Domain.Dto.UserDto;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(Roles = nameof(Position.User))]
public class UserSettingsController : ControllerBase
{
    IUserSettingsService<ResponceDto> _service;

    public UserSettingsController(IUserSettingsService<ResponceDto> service)
    {
        _service = service;
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUsersSettings(UserSettingsDto dto)
    {
        var result = await _service.UpdateUsersSettings(dto);
        if(result.Status == "error")
            return BadRequest("Malumotlar to'ldirilmagan");
        return Ok(result);
    }
}
﻿using Domain.Dto;
using Domain.Dto.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class UsersSettingsController : ControllerBase
{
    IUserSettingsService<ResponceDto> _service;

    public UsersSettingsController(IUserSettingsService<ResponceDto> service)
    {
        _service = service;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUsersSettingsAsync(UserSettingsDto dto)
    {
        var result = await _service.UpdateUsersSettingsAsync(dto);
        if (result.Status == "error 404")
            return NotFound(result.Message);
        if (result.Status == "error 400")
            return BadRequest(result.Message);
        if (result.Status == "error 500")
            return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

        return Ok(result);
    }
}
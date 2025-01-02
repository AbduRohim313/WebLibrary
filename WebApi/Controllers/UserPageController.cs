using Domain.Dto.UserDto;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = nameof(Position.User))]
public class UserPageController : ControllerBase
{
    private readonly IOstonaService<BookDto> _service;


    public UserPageController(IOstonaService<BookDto> service)
    {
        _service = service;
    }


    [HttpPut("KitobObKetiw/{id}")]
    public async Task<IActionResult> KitobObKetishAsync(int id)
    {
        var data = await _service.KitobObKetishAsync(id);
        if (data == null)
            return NotFound();
        return Ok(data);
    }

    [HttpPut("KitobQaytarish/{id}")]
    public async Task<IActionResult> KitobQaytarishAsync(int id)
    {
        var data = await _service.KitobQaytarishAsync(id);
        if (data == null)
            return NotFound();
        return Ok(data);
    }


    [HttpGet("allUsersBooks")]
    public async Task<IActionResult> AllUsersBooksAsync()
    {
        return Ok(await _service.ToplamAsync());
    }
}
using Domain.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserPageController : ControllerBase
{
    private readonly IOstonaService<BookDto> _service;


    public UserPageController(IOstonaService<BookDto> service)
    {
        _service = service;
    }


    [HttpPut("KitobObKetiw/{id}")]
    public async Task<IActionResult> KitobObKetish(int id)
    {
        var data = await _service.KitobObKetish(id);
        if (data == null)
            return NotFound();
        return Ok(data);
    }

    [HttpPut("KitobQaytarish/{id}")]
    public async Task<IActionResult> KitobQaytarish(int id)
    {
        var data = await _service.KitobQaytarish(id);
        if (data == null)
            return NotFound();
        return Ok(data);
    }


    [HttpGet("allusersbooks")]
    public async Task<IActionResult> AllUsersBooks()
    {
        return Ok(await _service.Toplam());
    }
}
using Domain.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserPageController : ControllerBase
{
    private readonly IOstonaService<BookDto> _service;

    // Внедрение сервиса через DI
    public UserPageController(IOstonaService<BookDto> service)
    {
        _service = service;
    }

    // Метод для получения данных
    [HttpPut("KitobObKetiw/{id}")]
    public async Task<IActionResult> KitobObKetish(int id)
    {
        var data = await _service.KitobObKetish(id);
        return Ok(data);
    }

    [HttpPut("KitobQaytarish/{id}")]
    public async Task<IActionResult> KitobQaytarish(int id)
    {
        var data = await _service.KitobQaytarish(id);
        return Ok(data);
    }
    
    

    // Метод для сохранения данных
    [HttpGet("allusersbooks")]
    public async Task<IActionResult> AllUsersBooks()
    {
        return Ok(await _service.Toplam());
    }
}
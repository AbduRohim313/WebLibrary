using Domain.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OstonaController : ControllerBase
{
    private readonly IOstonaService<BookDto> _service;

    // Внедрение сервиса через DI
    public OstonaController(IOstonaService<BookDto> service)
    {
        _service = service;
    }

    // Метод для получения данных
    [HttpGet("KitobObKetiw/{id}")]
    public async Task<IActionResult> KitobObKetish(int id)
    {
        var data = await _service.KitobObKetish(id);
        return Ok(data);
    }
    
    

    // Метод для сохранения данных
    // [HttpPost]
    // public async Task<IActionResult> SaveData([FromBody] SomeModel model)
    // {
    //     await _service.SaveDataAsync(model);
    //     return Ok();
    // }
}
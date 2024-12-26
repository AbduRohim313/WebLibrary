using Domain.Dto;
using Domain.Dto.UserDto;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("adminPage/[controller]")]
public class LibraryBookController : ControllerBase
{
    private IRDWithCRUDService<LibraryBookDto, int> _rdWithCrudService;
    ICreateService<LibraryBookDto> _createService;

    public LibraryBookController(IRDWithCRUDService<LibraryBookDto, int> rdWithCrudService,
        ICreateService<LibraryBookDto> createService)
    {
        _rdWithCrudService = rdWithCrudService;
        _createService = createService;
    }

    [HttpGet]
    public async Task<IEnumerable<LibraryBookDto>> GetAll()
    {
        return await _rdWithCrudService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<LibraryBookDto> GetById(int id)
    {
        return await _rdWithCrudService.GetById(id);
    }

    [HttpPost]
    [Authorize(Roles = nameof(Position.Admin))]
    public async Task<ResponceDto> Post(LibraryBookDto dto)
    {
        var responceDto = await _createService.Create(dto);
        return responceDto;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(Position.Admin))]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _rdWithCrudService.Delete(id);
        if (result)
            return Ok("O'chirildi");
        return BadRequest("O'chirilmadi");
    }
}
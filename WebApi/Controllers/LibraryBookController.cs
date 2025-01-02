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
    public async Task<IEnumerable<LibraryBookDto>> GetAllAsync()
    {
        return await _rdWithCrudService.ReadAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<LibraryBookDto> GetByIdAsync(int id)
    {
        return await _rdWithCrudService.ReadByIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = nameof(Position.Admin))]
    public async Task<ResponceDto> PostAsync(LibraryBookDto dto)
    {
        var responceDto = await _createService.CreateAsync(dto);
        return responceDto;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(Position.Admin))]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _rdWithCrudService.DeleteAsync(id);
        if (result)
            return Ok("O'chirildi");
        return NotFound("Topilmadi");
    }
}
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    IService<AuthorDto> _authorService;

    public AuthorController(IService<AuthorDto> authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        return Ok(await _authorService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        return Ok(await _authorService.GetById(id));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto authorDto)
    {
        var createdAuthor = await _authorService.Create(authorDto);
        var values = createdAuthor.Id;
        return CreatedAtRoute(values, createdAuthor);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthor([FromBody] AuthorDto authorDto)
    {
        var result = await _authorService.Update(authorDto);
        return result != null ? Ok("mufiaqatli ozgartirildi") : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
       return await _authorService.Delete(id) ? NoContent() : NotFound();
    }
}
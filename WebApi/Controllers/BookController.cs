using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    IService<BookDto> _bookService;

    public BookController(IService<BookDto> bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _bookService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _bookService.GetById(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookDto bookDto)
    {
        var result = await _bookService.Create(bookDto);
        return CreatedAtRoute(result.BookId, result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] BookDto bookDto)
    {
        var result = await _bookService.Update(bookDto);
        return result != null ? Ok("mofiaqatli ozgartirildi") : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _bookService.Delete(id) ? NoContent() : NotFound();
    }
}
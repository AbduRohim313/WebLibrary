using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Service;

namespace WebApi.Controllers;

public class SomeController : ControllerBase
{
    private readonly LibraryBookService _libraryBookService;

    public SomeController(LibraryBookService libraryBookService)
    {
        _libraryBookService = libraryBookService;
    }

    [HttpPost("AddLibraryBook")]
    public async Task<IActionResult> AddLibraryBook(LibraryBook libraryBook)
    {
        await _libraryBookService.AddLibraryBookAsync(libraryBook);
        return Ok();
    }
}
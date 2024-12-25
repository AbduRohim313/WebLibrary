using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[Route("adminPage/[controller]CRUD")]
// [Route("[controller]")]
[ApiController]
[Authorize(Roles = nameof(Position.Admin))]
public class UserController : ControllerBase
{
    private IRDWithCRUD<UserDto> _rdWithCrud;
    private IUpdate<UserDto> _update;

    public UserController(IUpdate<UserDto> update, IRDWithCRUD<UserDto> rdWithCrud)
    {
        _update = update;
        _rdWithCrud = rdWithCrud;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(_rdWithCrud.GetAll());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _rdWithCrud.GetById(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }
    //
    // [HttpPost]
    // public async Task<IActionResult> Post(LoginDto data)
    // {
    //     var responce = await _rdWithCrud.Create(data);
    //     if (responce == null)
    //     {
    //         return StatusCode(StatusCodes.Status400BadRequest, new ResponceDto()
    //         {
    //             Message = "bunday foydalanuvchi bor!",
    //             Status = "error"
    //         });
    //     }
    //
    //     if (responce.Status == "success")
    //     {
    //         return Ok(responce);
    //     }
    //
    //     return StatusCode(StatusCodes.Status500InternalServerError);
    // }
    
    [HttpPut]
    public async Task<IActionResult> Put(UserDto data)
    {
        var responce = await _update.Update(data);
        if (responce == null)
            return StatusCode(StatusCodes.Status400BadRequest, new ResponceDto()
            {
                Message = "bunday foydalanuvchi yoq!",
            });
        if (responce.Status == "Success")
        {
            return Ok(responce);
        }
    
        if (responce.Status == "error")
        {
            BadRequest(responce.Message);
        }
    
        return StatusCode(StatusCodes.Status500InternalServerError);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return await _rdWithCrud.Delete(id) ? Ok("deleted") : BadRequest();
    }
    
    // public async Task<ResponceDto> RemoveBookFromUser(string userId, int bookId)
    // {
    //     try
    //     {
    //         // Найти книгу по её ID и UserId
    //         var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId && b.UserId == userId);
    //         if (book == null)
    //         {
    //             return new ResponceDto
    //             {
    //                 Status = "error",
    //                 Message = "Книга не найдена у данного пользователя"
    //             };
    //         }
    //
    //         // Удалить книгу
    //         _dbContext.Books.Remove(book);
    //         await _dbContext.SaveChangesAsync();
    //
    //         return new ResponceDto
    //         {
    //             Status = "success",
    //             Message = "Книга успешно удалена у пользователя"
    //         };
    //     }
    //     catch (Exception ex)
    //     {
    //         // Логирование ошибки
    //         Console.WriteLine($"Ошибка при удалении книги: {ex.Message}");
    //         return new ResponceDto
    //         {
    //             Status = "error",
    //             Message = "Произошла ошибка при удалении книги"
    //         };
    //     }
    // }
    //
    // [HttpDelete("users/{userId}/books/{bookId}")]
    // public async Task<IActionResult> RemoveBookFromUser(string userId, int bookId)
    // {
    //     var response = await _rdWithCrud.(userId, bookId);
    //
    //     if (response.Status == "error")
    //         return BadRequest(response);
    //
    //     return Ok(response);
    // }
}
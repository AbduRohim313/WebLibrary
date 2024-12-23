using System.Security.Claims;
using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Interface;
using Domain.Repository;
using Microsoft.AspNetCore.Identity;
using WebApi.Interface;

namespace WebApi.Service;

public class OstonaService : IOstonaService<BookDto>
{
    IRepository<LibraryBook> _repository;
    UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OstonaService(IRepository<LibraryBook> repository,
        IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
public async Task<BookDto> KitobObKetish(int id)
{
    var book = await _repository.GetByIdAsync(id);
    if (book == null)
        return null;

    // Check if HttpContext and User are not null
    var httpContext = _httpContextAccessor.HttpContext;
    if (httpContext == null)
        throw new Exception("HttpContext is null!");

    var user = httpContext.User;
    if (user == null)
        throw new Exception("User is null!");

    var userIdClaim = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
        throw new Exception("User ID not found in token!");

    var userId = userIdClaim.Value;

    // Logic to add book to user and remove from library
    var userEntity = _userManager.Users.FirstOrDefault(x => x.Id == userId);
    if (userEntity == null)
        throw new Exception("User not found!");

    userEntity.Books.Add(new Book()
    {
        FullName = book.FullName,
        User = userEntity
    });
    await _repository.Delete(book.BookId);
    return new BookDto
    {
        BookId = book.BookId,
        Name = book.FullName
    };
}
    public Task<BookDto> KitobQaytarish(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookDto>> Toplam()
    {
        throw new NotImplementedException();
    }
}
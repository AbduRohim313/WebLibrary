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
    IRepository<LibraryBook> _libraryRepository;
    UserManager<User> _userManager;
    IRemoveByUser _bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OstonaService(IRepository<LibraryBook> libraryRepository,
        IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IRemoveByUser bookRepository)
    {
        _libraryRepository = libraryRepository;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _bookRepository = bookRepository;
    }

    private async Task<User> GetUserByClaimAsync()
    {
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
        return userEntity;
    }

    public async Task<BookDto> KitobObKetish(int id)
    {
        var book = await _libraryRepository.GetByIdAsync(id);
        if (book == null)
            return null;

        var userEntity = await GetUserByClaimAsync();

        userEntity.Books.Add(new Book()
        {
            FullName = book.FullName,
            User = userEntity
        });
        await _libraryRepository.Delete(book.BookId);
        return new BookDto()
        {
            Name = book.FullName,
            BookId = book.BookId
        };
    }

    public async Task<BookDto> KitobQaytarish(int id)
    {
        var userEntity = await GetUserByClaimAsync();
        var book = userEntity.Books.FirstOrDefault(x => x.BookId == id);
        if (book == null)
            return null;
        // await _bookRepository.RemoveByUser(userEntity, id); // add bn remove iwlamayapti
        userEntity.Books.Remove(book);
        var result = await _userManager.UpdateAsync(userEntity);
        if (!result.Succeeded)
            return null;
        await _libraryRepository.Add(new LibraryBook()
        {
            FullName = book.FullName,
        });
        return new BookDto()
        {
            Name = book.FullName,
            BookId = book.BookId
        };
    }

    public Task<IEnumerable<BookDto>> Toplam()
    {
        throw new NotImplementedException();
    }
}
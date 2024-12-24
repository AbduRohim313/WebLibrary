using System.Security.Claims;
using Domain;
using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Interface;
using Domain.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Interface;

namespace WebApi.Service;

public class OstonaService : IOstonaService<BookDto>
{
    IRepository<LibraryBook> _libraryRepository;
    UserManager<User> _userManager;
    IRepository<Book> _bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OstonaService(IRepository<LibraryBook> libraryRepository,
        IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IRepository<Book> bookRepository)
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
        // var userEntity = _userManager.Users.FirstOrDefault(x => x.Id == userId);
        var userEntity = _userManager.Users
            .Include(u => u.Books) // Подключение навигационного свойства
            .FirstOrDefault(x => x.Id == userId);
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
        _userManager.UpdateAsync(userEntity).Wait();
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
        var books = userEntity.Books;
        var book = books.FirstOrDefault(x => x.BookId == id);
        if (book == null)
            return null;
        // await _bookRepository.RemoveByUser(userEntity, id); // add bn remove iwlamayapti
        var result = userEntity.Books.Remove(book);
        // var result = await _userManager.UpdateAsync(userEntity);
        // var result = await _bookRepository.RemoveByUser(userEntity, id);
        if (result == false)
            return null;
        await _libraryRepository.Add(new LibraryBook()
        {
            FullName = book.FullName,
        });
        await _bookRepository.Delete(book.BookId);
        return new BookDto()
        {
            Name = book.FullName,
            BookId = book.BookId
        };
    }

    public async Task<IEnumerable<BookDto>> Toplam()
    {
        var user = await GetUserByClaimAsync();
        var result = new List<BookDto>();
        foreach (var book in user.Books)
        {
            result.Add(new BookDto()
            {
                Name = book.FullName,
                BookId = book.BookId
            });
        }

        return result;
    }
}
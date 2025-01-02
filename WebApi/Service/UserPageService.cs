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

public class UserPageService : IOstonaService<BookDto>
{
    // IRepository<LibraryBook> _libraryRepository;
    IGetRemoveRepository<LibraryBook> _irdRepository;
    IAddRepository<LibraryBook> _iAddRepository;
    UserManager<User> _userManager;
    IRepository<Book> _bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserPageService(
        IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IRepository<Book> bookRepository,
        IGetRemoveRepository<LibraryBook> irdRepository, IAddRepository<LibraryBook> iAddRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _bookRepository = bookRepository;
        _irdRepository = irdRepository;
        _iAddRepository = iAddRepository;
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
            .Include(u => u.Books) 
            .FirstOrDefault(x => x.Id == userId);
        if (userEntity == null)
            throw new Exception("User not found!");
        return userEntity;
    }

    public async Task<BookDto> KitobObKetish(int id)
    {
        var book = await _irdRepository.GetByIdAsync(id);
        if (book == null)
            return null;

        var userEntity = await GetUserByClaimAsync();

        userEntity.Books.Add(new Book()
        {
            FullName = book.FullName,
            Author = book.Author,
            User = userEntity
        });
        _userManager.UpdateAsync(userEntity).Wait();
        await _irdRepository.RemoveAsync(book.BookId);
        return new BookDto()
        {
            BookId = book.BookId,
            Name = book.FullName,
            Author = book.Author
        };
    }

    public async Task<BookDto> KitobQaytarish(int id)
    {
        var userEntity = await GetUserByClaimAsync();
        var books = userEntity.Books;
        var book = books.FirstOrDefault(x => x.BookId == id);
        if (book == null)
            return null;
        // await _bookRepository.RemoveByUser(userEntity, id);
        var result = userEntity.Books.Remove(book);
        // var result = await _userManager.UpdateAsync(userEntity);
        // var result = await _bookRepository.RemoveByUser(userEntity, id);
        if (result == false)
            return null;
        await _iAddRepository.AddAsync(new LibraryBook()
        {
            FullName = book.FullName,
            Author = book.Author
        });
        await _bookRepository.RemoveAsync(book.BookId);
        return new BookDto()
        {
            BookId = book.BookId,
            Name = book.FullName,
            Author = book.Author
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
                BookId = book.BookId,
                Name = book.FullName,
                Author = book.Author
            });
        }

        return result;
    }
}
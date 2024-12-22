using System.Security.Claims;
using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Interface;
using Domain.Repository;
using WebApi.Interface;

namespace WebApi.Service;

public class OstonaService : IOstonaService<BookDto>
{
    IRepository<LibraryBook> _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OstonaService(IRepository<LibraryBook> repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BookDto> KitobObKetish(int id)
    {
        var book = await _repository.GetById(id);
        if (book == null)
            return null;

        // Получение UserId из токена
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            throw new Exception("User ID not found in token!");

        var userId = int.Parse(userIdClaim.Value);

        // Логика для добавления книги пользователю и удаления из библиотеки
        // ...

        return new BookDto
        {
            BookId = book.BookId,
            Name = book.FullName
        };
    } // Получите идентификатор текущего пользователя

    public Task<BookDto> KitobQaytarish(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookDto>> Toplam()
    {
        throw new NotImplementedException();
    }
}
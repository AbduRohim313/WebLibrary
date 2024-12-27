using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Interface;
using WebApi.Interface;

namespace WebApi.Service;

public class UpdateUsersBookForAdminService : IUpdateUsersBookForAdmin<BookDto>
{
    IRepository<Book> _bookRepository;

    public UpdateUsersBookForAdminService(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> Create(string userId, BookDto dto)
    {
        // ozi bunaqa kitob bomi yomi?

        var book = _bookRepository.GetByIdAsync(dto.BookId);
        if (book == null)
            return null;
        var responce = await _bookRepository.Add(new Book()
        {
            BookId = dto.BookId,
            FullName = dto.Name,
            Author = dto.Author,
            UserId = userId
        });
        return new BookDto()
        {
            BookId = responce.BookId,
            Name = responce.FullName,
            Author = responce.Author
        };
    }


    public async Task<bool> Delete(int bookId)
    {
        return await _bookRepository.Delete(bookId);
    }
}
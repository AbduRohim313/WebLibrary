using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Interface;
using WebApi.Interface;

namespace WebApi.Service;

public class BookService : IService<BookDto>
{
    IRepository<Book> _repository;

    public BookService(IRepository<Book> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<BookDto>> ReadAllAsync()
    {
        var responce = await _repository.GetAllAsync();
        var result = new List<BookDto>();
        foreach (var book in responce)
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

    public async Task<BookDto> ReadByIdAsync(int id)
    {
        var responce = await _repository.GetByIdAsync(id);
        return new BookDto()
        {
            BookId = responce.BookId,
            Name = responce.FullName,
            Author = responce.Author
        };
    }

    public async Task<BookDto> CreateAsync(BookDto data)
    {
        var responce = await _repository.AddAsync(new Book()
        {
            FullName = data.Name,
            Author = data.Author
        });
        return new BookDto()
        {
            BookId = responce.BookId,
            Name = responce.FullName,
            Author = responce.Author
        };
    }

    public async Task<BookDto> UpdateAsync(BookDto data)
    {
        var responce = await _repository.GetByIdAsync(data.BookId);
        if (responce == null)
            return null;
        responce.FullName = data.Name;
        responce.Author = data.Author;
        await _repository.UpdateAsync(responce);
        return data;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if(null == await _repository.GetByIdAsync(id))
            return false;
        await _repository.RemoveAsync(id);
        return true;
    }
}
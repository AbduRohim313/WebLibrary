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

    public async Task<IEnumerable<BookDto>> GetAll()
    {
        var responce = await _repository.GetAll();
        var result = new List<BookDto>();
        foreach (var book in responce)
        {
            result.Add(new BookDto()
            {
                Name = book.FullName,
                BookId = book.BookId,
            });
        }
        return result;
    }

    public async Task<BookDto> GetById(int id)
    {
        var responce = await _repository.GetByIdAsync(id);
        return new BookDto()
        {
            Name = responce.FullName,
            BookId = responce.BookId,
        };
    }

    public async Task<BookDto> Create(BookDto data)
    {
        var responce = await _repository.Add(new Book()
        {
            FullName = data.Name,
        });
        return new BookDto()
        {
            Name = responce.FullName,
            BookId = responce.BookId,
        };
    }

    public async Task<BookDto> Update(BookDto data)
    {
        var responce = await _repository.GetByIdAsync(data.BookId);
        if (responce == null)
            return null;
        responce.FullName = data.Name;
        await _repository.Update(responce);
        return data;
    }

    public async Task<bool> Delete(int id)
    {
        if(null == await _repository.GetByIdAsync(id))
            return false;
        await _repository.Delete(id);
        return true;
    }
}
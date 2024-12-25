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

    public Task<BookDto> Create(BookDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
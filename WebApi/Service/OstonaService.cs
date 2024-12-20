using Domain.Dto.UserDto;
using WebApi.Interface;

namespace WebApi.Service;

public class OstonaService : IOstonaService<BookDto>
{
    public Task<BookDto> KitobObKetish(int id)
    {
        throw new NotImplementedException();
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
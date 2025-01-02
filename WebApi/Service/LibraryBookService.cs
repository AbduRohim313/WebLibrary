using Domain;
using Domain.Dto;
using Domain.Dto.UserDto;
using Domain.Interface;
using WebApi.Interface;

namespace WebApi.Service;

using Domain.Entity;
using Microsoft.EntityFrameworkCore;

public class LibraryBookService : IRDWithCRUDService<LibraryBookDto, int>, ICreateService<LibraryBookDto>
{
    IGetRemoveRepository<LibraryBook> _irdRepository;
    IAddRepository<LibraryBook> _iAddRepository;

    public LibraryBookService(IGetRemoveRepository<LibraryBook> irdRepository, IAddRepository<LibraryBook> iAddRepository)
    {
        _irdRepository = irdRepository;
        _iAddRepository = iAddRepository;
    }


    public async Task<IEnumerable<LibraryBookDto>> ReadAllAsync()
    {
       var books = await _irdRepository.GetAllAsync();
         return books.Select(x => new LibraryBookDto()
         {
             Id = x.BookId,
             FullName = x.FullName,
             Author = x.Author
         });
    }

    public async Task<LibraryBookDto> ReadByIdAsync(int id)
    {
        var book = await _irdRepository.GetByIdAsync(id);
        if(book == null)
            return null;
        return new LibraryBookDto()
        {
            FullName = book.FullName,
            Author = book.Author
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if(await _irdRepository.GetByIdAsync(id) == null)
            return false;
        return await _irdRepository.RemoveAsync(id);
    }

    public async Task<ResponceDto> CreateAsync(LibraryBookDto dto)
    {
        var book = await _iAddRepository.AddAsync(new LibraryBook()
        {
            FullName = dto.FullName,
            Author = dto.Author,
        });
        return new ResponceDto()
        {
            Message = "Kitob qo'shildi",
            Status = "Success"
        };
    }
}
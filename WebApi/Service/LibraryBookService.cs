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
    IRDRepository<LibraryBook> _irdRepository;
    ICreateRepository<LibraryBook> _createRepository;

    public LibraryBookService(IRDRepository<LibraryBook> irdRepository, ICreateRepository<LibraryBook> createRepository)
    {
        _irdRepository = irdRepository;
        _createRepository = createRepository;
    }


    public async Task<IEnumerable<LibraryBookDto>> GetAll()
    {
       var books = await _irdRepository.GetAll();
         return books.Select(x => new LibraryBookDto()
         {
             Id = x.BookId,
             FullName = x.FullName,
             Author = x.Author
         });
    }

    public async Task<LibraryBookDto> GetById(int id)
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

    public async Task<bool> Delete(int id)
    {
        if(await _irdRepository.GetByIdAsync(id) == null)
            return false;
        return await _irdRepository.Delete(id);
    }

    public async Task<ResponceDto> Create(LibraryBookDto dto)
    {
        var book = await _createRepository.Add(new LibraryBook()
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
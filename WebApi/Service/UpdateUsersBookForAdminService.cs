using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using WebApi.Interface;

namespace WebApi.Service;

public class UpdateUsersBookForAdminService : IUpdateUsersBookForAdmin<BookDto>
{
    IRepository<Book> _bookRepository;
    private UserManager<User> _userManager;

    public UpdateUsersBookForAdminService(IRepository<Book> bookRepository, UserManager<User> userManager)
    {
        _bookRepository = bookRepository;
        _userManager = userManager;
    }

    public async Task<ResponceDto> CreateAsync(string userId, BookDto dto)
    {
        // ozi bunaqa kitob bomi yomi?

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new ResponceDto()
            {
                Message = "Foydalanuvchi topilmadi!",
                Status = "error 404"
            };

        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Author))
            return new ResponceDto()
            {
                Message = "Malumotlar to'ldirilmadi!",
                Status = "error 400"
            };

        var responce = await _bookRepository.AddAsync(new Book()
        {
            BookId = dto.BookId,
            FullName = dto.Name,
            Author = dto.Author,
            UserId = userId
        });
        return new ResponceDto()
        {
            Message = "Kitob qo'shildi!",
            Status = "success"
        };
    }


    public async Task<bool> DeleteAsync(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null)
            return false;
        return await _bookRepository.RemoveAsync(bookId);
    }
}
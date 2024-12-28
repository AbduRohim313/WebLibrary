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
    private IDetach<Book> _detach;

    public UpdateUsersBookForAdminService(IRepository<Book> bookRepository, UserManager<User> userManager,
        IDetach<Book> detach)
    {
        _bookRepository = bookRepository;
        _userManager = userManager;
        _detach = detach;
    }

    public async Task<ResponceDto> Create(string userId, BookDto dto)
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

        var responce = await _bookRepository.Add(new Book()
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


    public async Task<bool> Delete(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null)
            return false;
        return await _bookRepository.Delete(bookId);
    }
}
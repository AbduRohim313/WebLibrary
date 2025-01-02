using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Interface;
using WebApi.Interface;

namespace WebApi.Service;

public class AuthorService : IService<AuthorDto>
{
    IRepository<Author> _repository;

    public AuthorService(IRepository<Author> repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<AuthorDto>> ReadAllAsync()
    {
        var all = await _repository.GetAllAsync();
        var result = new List<AuthorDto>();
        foreach (var author in all)
        {
            result.Add(new AuthorDto
            {
                Id = author.AuthorId,
                FullName = author.FullName,
            });
        }

        return result;
    }

    public async Task<AuthorDto> ReadByIdAsync(int id)
    {
        var byId = await _repository.GetByIdAsync(id);
        return new AuthorDto()
        {
            Id = byId.AuthorId,
            FullName = byId.FullName,
        };
    }

    public async Task<AuthorDto> CreateAsync(AuthorDto data)
    {
        var author = new Author
        {
            FullName = data.FullName,
        };
        var responce = await _repository.AddAsync(author);
        return new AuthorDto()
        {
            Id = responce.AuthorId,
            FullName = responce.FullName,
        };
    }

    public async Task<AuthorDto?> UpdateAsync(AuthorDto data)
    {
        var author = await _repository.GetByIdAsync(data.Id);
        if (author == null)
            return null;
        author.FullName = data.FullName;
        var result = await _repository.UpdateAsync(author);
        return new AuthorDto()
        {
            Id = result.AuthorId,
            FullName = result.FullName,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (await _repository.GetByIdAsync(id) is not null)
        {
            return await _repository.RemoveAsync(id);
        }
        return false;
    }
}
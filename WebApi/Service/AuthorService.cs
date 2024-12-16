using Domain.Dto;
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


    public async Task<IEnumerable<AuthorDto>> GetAll()
    {
        var all = await _repository.GetAll();
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

    public async Task<AuthorDto> GetById(int id)
    {
        var byId = await _repository.GetById(id);
        return new AuthorDto()
        {
            Id = byId.AuthorId,
            FullName = byId.FullName,
        };
    }

    public async Task<AuthorDto> Create(AuthorDto data)
    {
        var author = new Author
        {
            FullName = data.FullName,
        };
        var responce = await _repository.Add(author);
        return new AuthorDto()
        {
            Id = responce.AuthorId,
            FullName = responce.FullName,
        };
    }

    public async Task<AuthorDto?> Update(AuthorDto data)
    {
        var author = await _repository.GetById(data.Id);
        if (author == null)
            return null;
        author.FullName = data.FullName;
        var result = await _repository.Update(author);
        return new AuthorDto()
        {
            Id = result.AuthorId,
            FullName = result.FullName,
        };
    }

    public async Task<bool> Delete(int id)
    {
        if (await _repository.GetById(id) is not null)
        {
            return await _repository.Delete(id);
        }
        return false;
    }
}
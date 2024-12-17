using Domain.Entity;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Service;

public class UserService : IService<User>
{
    public Task<IEnumerable<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<User>? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> Create(User data)
    {
        throw new NotImplementedException();
    }

    public Task<User> Update(User data)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
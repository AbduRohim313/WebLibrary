﻿using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

// [Route("[controller]")]
// [ApiController]
public class UserController : ControllerBase
{
    UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
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
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    IService<User> _userService;

    public UserController(IService<User> userService)
    {
        _userService = userService;
    }
}
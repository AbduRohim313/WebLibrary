using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    IService<Author> _service;

    public AuthController(IService<Author> service)
    {
        _service = service;
    }
}
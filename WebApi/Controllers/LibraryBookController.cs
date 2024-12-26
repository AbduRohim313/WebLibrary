using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("adminPage/[controller]")]
[Authorize(Roles = nameof(Position.Admin))]
public class LibraryBookController : ControllerBase
{
    
}
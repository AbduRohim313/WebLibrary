using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Dto;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    UserManager<User> userManager;
    IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var user = await userManager.FindByNameAsync(registerDto.UserName);
        if (user != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponceDto()
            {
                Message = "User topiladi",
                Status = "error"
            });
        }

        var newUser = new User()
        {
            UserName = registerDto.UserName,
            PasswordHash = registerDto.Password,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponceDto()
            {
                Status = "error",
                Message = "user yaratilmadi",
            });
        }

        return Ok(new ResponceDto() { Status = "Success", Message = "User mufiaqatli yaratildi" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        var user = await userManager.FindByNameAsync(userDto.UserName);
        if (user != null && await userManager.CheckPasswordAsync(user, userDto.Password))
        {
            var roles = await userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            Claim claim = new Claim(ClaimTypes.Name, userDto.UserName);
            Claim claimId = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims.Add(claim);
            claims.Add(claimId);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(_configuration["JWT:Issur"], _configuration["JWT:Audience"],
                claims, expires: DateTime.Now.AddMinutes(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
        }

        return Unauthorized();
    }
}
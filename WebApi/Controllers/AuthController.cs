﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain;
using Domain.Dto;
using Domain.Dto.UserDto;
using Domain.Entity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Interface;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    UserManager<User> _userManager;
    IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;


    public AuthController(UserManager<User> userManager, IConfiguration configuration,
        RoleManager<IdentityRole> roleManager)
    {
        this._userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
    {
        var user = await _userManager.FindByNameAsync(registerDto.UserName);
        if (user != null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponceDto()
            {
                Message = "Bu username mavjud",
                Status = "error"
            });
        }

        var newUser = new User()
        {
            UserName = registerDto.UserName,
            Books = new List<Book>()
        };

        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponceDto()
            {
                Status = "error",
                Message = "user yaratilmadi",
            });
        }


        var addToRoleResult = await _userManager.AddToRoleAsync(newUser, Position.User.ToString());
        if (!addToRoleResult.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponceDto()
            {
                Status = "error",
                Message = "Роль не добавлена: " + string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))
            });
        }

        return Ok(new ResponceDto() { Status = "Success", Message = "User mufiaqatli yaratildi" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            Claim claim = new Claim(ClaimTypes.Name, loginDto.UserName);
            // Claim claimPosition = new Claim(ClaimTypes.Role, roleUser.FirstOrDefault()??"User");
            Claim claimId = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims.Add(claim);
            claims.Add(claimId);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(_configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            // return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        return StatusCode(StatusCodes.Status401Unauthorized, new ResponceDto()
        {
            Status = "error",
            Message = "Invalid username or password"
        });
    }

    [HttpGet("test-role")]
    // public IActionResult TestRole()
    // {
    //     var userRole = User.Claims.FirstOrDefault(c => c.Value == ClaimTypes.Role)?.Value;
    //     return Ok($"User role: {userRole}");
    // }
    public async Task<IActionResult> TestRoleAsync()
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(userRole))
        {
            return NotFound(new { Status = "Error", Message = "User role not found" });
        }
    
        return Ok(new { Status = "Success", Role = userRole });
    }

    [HttpPut("updateToAdmin")]
    [Authorize(Roles = nameof(Position.Admin))]
    public async Task<IActionResult> UpdateToAdminAsync(ToAdminDto toAdminDto)
    {
        // Найти пользователя
        var user = await _userManager.FindByNameAsync(toAdminDto.Name);
        if (user == null)
        {
            return NotFound(new { Status = "Error", Message = "User not found" });
        }

        // Удалить пользователя из роли "User", если он в ней
        if (await _userManager.IsInRoleAsync(user, Position.User.ToString()))
        {
            var removeResult = await _userManager.RemoveFromRoleAsync(user, Position.User.ToString());
            if (!removeResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = "Error",
                    Message = "Failed to remove User role"
                });
            }
        }
        // Добавить пользователя в роль "Admin"
        var addResult = await _userManager.AddToRoleAsync(user, Position.Admin.ToString());
        if (!addResult.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Status = "Error",
                Message = "Failed to add Admin role"
            });
        }

        return Ok(new { Status = "Success", Message = $"{toAdminDto.Name} is now an Admin" });
    }
}
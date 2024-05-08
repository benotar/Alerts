using System.Security.Cryptography;
using System.Text;
using Alerts.Domain.Entities.AuthModels;
using Alerts.Domain.Entities.Database;
using Alerts.Domain.Entities.Database.Dtos;
using Alerts.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Alerts.WebApp.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _db;

    public AuthController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] AuthenticationModel authModel)
    {
        if (authModel is null)
        {
            return BadRequest("Incorrect input data!");
        }

        if (_db.Users.Where(u => u.UserName == authModel.UserName).FirstOrDefault() != null)
        {
            return BadRequest($"User \'{authModel.UserName}\' already exist!");
        }
        
        var user = new User { UserName = authModel.UserName, Name = authModel.Name, Role = "User"};

        if (authModel.ConfirmPassword == authModel.Password)
        {
            using (HMACSHA256 hmac = new())
            {
                user.PasswordSalt = hmac.Key;

                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(authModel.Password));
            }
        }
        else
        {
            return BadRequest("Password don`t match!");
        }

        await _db.Users.AddAsync(user);

        await _db.SaveChangesAsync();

        var returnUser = new UserAuthenticationDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Name = user.Name,
            Role = user.Role,
            PasswordSalt = user.PasswordSalt,
            PasswordHash = user.PasswordHash,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
        
        return Ok(returnUser);
    }
}
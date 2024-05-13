using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Alerts.Application.Configurations;
using Alerts.Application.Hepler;
using Alerts.Domain.Entities.AuthModels;
using Alerts.Domain.Entities.Database;
using Alerts.Domain.Entities.Database.Dtos;
using Alerts.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Alerts.WebApp.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _db;

    private readonly JwtConfiguration _jwtConfiguration;
    
    public AuthController(ApplicationDbContext db, JwtConfiguration jwtConfiguration)
    {
        _db = db;

        _jwtConfiguration = jwtConfiguration;
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

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] AuthorizationModel authModel)
    {
        var user = await _db.Users.Where(u => u.UserName == authModel.UserName).FirstOrDefaultAsync();

        if (user is null)
        {
            return BadRequest($"The user \'{authModel.UserName}\' was not found in the database!");
        }

        var match = AuthHelper.CheckPassword(authModel.Password, user);

        if (!match)
        {
            return BadRequest("Password was invalid!");
        }

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, authModel.UserName),
            new(ClaimTypes.Role, user.Role)
        };

        var jwtToken = new JwtSecurityToken(
            issuer: "MyAlertServer",
            audience: "MyWpfAuthClient",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));

        var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        
        return Ok(tokenString);
    }
}
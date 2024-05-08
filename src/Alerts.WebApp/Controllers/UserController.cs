using Alerts.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alerts.WebApp.Controllers;

[ApiController]
[Route("userApi")]
public class UserController : Controller
{
    private readonly ApplicationDbContext _db;

    public UserController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("GetUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _db.Users.ToListAsync();

        if (users is null)
        {
            return NotFound("No users found in the database!");
        }
        
        
        return Ok(users);
    }
    
    [AllowAnonymous]
    [Authorize(Roles = "User")]
    [HttpPut("AddRegion/{userId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddRegion(long userId ,string region)
    {
        var user = await _db.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

        if (user is null)
        {
            return NotFound("User was not found in the database!");
        }
        
        user.Regions.Add(region);

        await _db.SaveChangesAsync();

        return NoContent();
    }
    
    [AllowAnonymous]
    [Authorize(Roles = "User")]
    [HttpDelete("DeleteRegion/{userId:long}/{userRegion}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRegion(long userId ,string userRegion)
    {
        var user = await _db.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

        if (user is null)
        {
            return NotFound("User was not found in the database!");
        }

        var region = user.Regions.FirstOrDefault(r => r == userRegion);
        
        if (string.IsNullOrEmpty(region))
        {
            return NotFound("Region was not found in the database!");
        }
        
        user.Regions.Remove(region);

        await _db.SaveChangesAsync();

        return NoContent();
    }
}
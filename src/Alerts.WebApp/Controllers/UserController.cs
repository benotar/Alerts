﻿using Alerts.Persistence;
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
    
    [AllowAnonymous]
    [Authorize(Roles = "User")]
    [HttpGet("GetUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsers()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated!");
        }
        
        var users = await _db.Users.ToListAsync();

        if (users is null)
        {
            return NotFound("No users found in the database!");
        }
        
        return Ok(users);
    }
    
    [AllowAnonymous]
    [Authorize(Roles = "User")]
    [HttpPut("AddRegion/{userId:long}/{userRegion}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddRegion(long userId ,string userRegion)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated!");
        }
        
        var user = await _db.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

        if (user is null)
        {
            return NotFound("User was not found in the database!");
        }

        if (string.IsNullOrEmpty(userRegion))
        {
            return BadRequest("The region cannot be empty!");
        }
        
        user.Regions.Add(userRegion);

        await _db.SaveChangesAsync();

        return NoContent();
    }
    
    [AllowAnonymous]
    [Authorize(Roles = "User")]
    [HttpDelete("DeleteRegion/{userId:long}/{userRegion}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRegion(long userId ,string userRegion)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated!");
        }
        
        var user = await _db.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

        if (user is null)
        {
            return NotFound("User was not found in the database!");
        }

        if (string.IsNullOrEmpty(userRegion))
        {
            return BadRequest("The region cannot be empty!");
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
using System.Net;
using System.Security.Claims;
using Alerts.Application.Alerts.Commands;
using Alerts.Application.Hepler;
using Alerts.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.String;

namespace Alerts.WebApp.Controllers;

[ApiController]
[Route("alertsApi")]
public class AlertsController : Controller
{
    private readonly IAlertsService _alertsService;

    public AlertsController(IAlertsService alertsService)
    {
        _alertsService = alertsService;
    }

    [HttpGet("GetActiveAlerts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetActiveAlerts()
    {
        var url = _alertsService.GetActiveAlertsUrl();

        if (IsNullOrEmpty(url))
        {
            return BadRequest();
        }

        GetActiveAlerts getActiveAlerts = new(url);

        var result = await getActiveAlerts.InvokeAsync();

        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }

    [AllowAnonymous]
    [Authorize(Roles = "User")]
    [HttpGet("GetActiveAlertsByOblasts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetActiveAlertsByOblasts()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated!");
        }

        string url = _alertsService.GetAlertsByOblasts();

        if (IsNullOrEmpty(url))
        {
            return BadRequest();
        }

        GetAlertsByOblasts getAlertsByOblasts = new(url);

        var result = await getAlertsByOblasts.InvokeAsync();

        if (IsNullOrEmpty(result))
        {
            return BadRequest();
        }

        return Ok(result);
    }

    
    [AllowAnonymous]
    [Authorize(Roles = "User")]
    [HttpGet("GetAlertByOblast")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAlertByOblast(int locationId)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated!");
        }
        
        var url = _alertsService.GetAlertByOblastIdUrl();
        
        if (IsNullOrEmpty(url))
        {
            return BadRequest();
        }

        var token = _alertsService.GetAlertsToken();
        
        if (IsNullOrEmpty(token))
        {
            return BadRequest();
        }

        if (!AlertsHelper.IsValidLocationId(locationId))
        {
            return BadRequest("Invalid location!");
        }
        
        GetAlertByOblastId getAlertByOblastId = new(url, locationId, token);

        var result = await getAlertByOblastId.InvokeAsync();
        
        if (IsNullOrEmpty(result))
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
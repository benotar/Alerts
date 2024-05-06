using Alerts.Application.Alerts.Commands;
using Alerts.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using static System.String;

namespace Alerts.WebApp.Controllers;

[ApiController]
[Route("alertsApi/[controller]")]
public class AlertsController : Controller
{
    private readonly IAlertsService _alertsService;

    public AlertsController(IAlertsService alertsService)
    {
        _alertsService = alertsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActiveAlerts()
    {
        string url = _alertsService.GetActiveAlertsUrl();

        if (IsNullOrEmpty(url))
        {
            return BadRequest();
        }
        
        GetActiveAlerts getActiveAlerts = new (url);

        var result = await getActiveAlerts.InvokeAsync();

        if (result is null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpGet("byOblasts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActiveAlertsByOblasts()
    {
        string url = _alertsService.GetAlertsByOblasts();
        
        if (IsNullOrEmpty(url))
        {
            return BadRequest();
        }

        GetAlertsByOblasts getAlertsByOblasts = new(url);

        var result = await getAlertsByOblasts.InvokeAsync();

        if (IsNullOrEmpty(result))
        {
            return NotFound();
        }

        return Ok(result);
    }
}
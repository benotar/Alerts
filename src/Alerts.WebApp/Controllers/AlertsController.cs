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
        var url = _alertsService.GetActiveAlertsUrl();

        if (IsNullOrEmpty(url))
        {
            return BadRequest();
        }
        
        var getActiveAlerts = new GetActiveAlerts(url);

        var result = await getActiveAlerts.GetAlertsAsync();

        if (result is null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
}
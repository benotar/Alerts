using Alerts.Application.Alerts.Commands;
using Alerts.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetActiveAlerts()
    {
        var url = _alertsService.GetActiveAlertsUrl();

        var getActiveAlerts = new GetActiveAlerts(url);

        var result = await getActiveAlerts.GetAlertsAsync();
        
        return Ok(result);
    }
}
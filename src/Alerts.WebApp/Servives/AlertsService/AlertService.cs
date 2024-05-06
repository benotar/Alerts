using Alerts.Application.Interfaces.Services;

namespace Alerts.WebApp.Servives.AlertsService;

public class AlertService(IConfiguration config) : IAlertsService
{
    public string GetAlertsToken()
    {
        return config.GetValue<string>("AlertsApi:AlertsToken");
    }

    public string GetActiveAlertsUrl()
    {
        return config.GetValue<string>("AlertsApi:GetFullAlertsMethodUrl");
    }
}
using Alerts.Application.Interfaces.Services;

namespace Alerts.WebApp.Servives.AlertsService;

public class AlertService : IAlertsService
{
    private readonly IConfiguration _config;

    public AlertService(IConfiguration config)
    {
        _config = config;
    }
    public string GetAlertsToken()
    {
        return _config.GetValue<string>("AlertsToken");
    }

    public string GetActiveAlertsUrl()
    {
        var result = _config.GetValue<string>("GetFullAlertsMethodUrl");

        //result += _config.GetValue<string>("AlertsToken");

        result += GetAlertsToken();

        return result;
    }

    public string GetAlertsByOblasts()
    {
        var result = _config.GetValue<string>("GetAlertsByOblastsUrl");

        //result += _config.GetValue<string>("AlertsToken");

        result += GetAlertsToken();
        
        return result;
    }
}
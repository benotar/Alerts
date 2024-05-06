namespace Alerts.Application.Interfaces.Services;

public interface IAlertsService
{
    string GetAlertsToken();
    string GetActiveAlertsUrl();
    string GetAlertsByOblasts();
}
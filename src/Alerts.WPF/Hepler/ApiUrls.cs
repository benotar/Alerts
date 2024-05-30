namespace Alerts.WPF.Hepler;

public static class ApiUrls
{
    public static string GetLoginUrl()
    => "https://localhost:44305/auth/Login";
    
    public static string GetRegisterUrl()
        => "https://localhost:44305/auth/Register";

    public static string GetActiveAlertsUrl()
        => "https://localhost:44305/alertsApi/GetActiveAlerts";
    
    public static string GetActiveAlertsByOblast(int oblastId)
    => $"https://localhost:44305/alertsApi/GetAlertByOblast?locationId={oblastId}";

    public static string GetDeleteRegionUrl(long userId, string region)
        => $"https://localhost:44305/userApi/DeleteRegion/{userId}/{region}";
    
    public static string GetAddRegionUrl(long userId, string region)
        => $"https://localhost:44305/userApi/AddRegion/{userId}/{region}";
}
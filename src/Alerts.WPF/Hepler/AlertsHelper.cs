using Alerts.WPF.AlertsModels;
using Alerts.WPF.AlertsModels.Enums;
using Alerts.WPF.Data.Models;

namespace Alerts.WPF.Hepler;

public static class AlertsHelper
{
    public static readonly Dictionary<int, string> AlertsLocation;

    static AlertsHelper()
    {
        AlertsLocation = new Dictionary<int, string>()
        {
            { 3, "Хмельницька область" },
            { 4, "Вінницька область" },
            { 5, "Рівненська область" },
            { 8, "Волинська область" },
            { 9, "Дніпропетровська область" },
            { 10, "Житомирська область" },
            { 11, "Закарпатська область" },
            { 12, "Запорізька область" },
            { 13, "Івано-Франківська область" },
            { 14, "Київська область" },
            { 15, "Кіровоградська область" },
            { 16, "Луганська область" },
            { 17, "Миколаївська область" },
            { 18, "Одеська область" },
            { 19, "Полтавська область" },
            { 20, "Сумська область" },
            { 21, "Тернопільська область" },
            { 22, "Харківська область" },
            { 23, "Херсонська область" },
            { 24, "Черкаська область" },
            { 25, "Чернігівська область" },
            { 26, "Чернівецька область" },
            { 27, "Львівська область" },
            { 28, "Донецька область" },
            { 29, "Автономна Республіка Крим" },
            { 30, "м. Севастополь" },
            { 31, "м. Київ" },
        };
    }

    public static bool IsValidOblastId(int oblastId)
        => AlertsLocation.ContainsKey(oblastId);

    public static bool IsValidLocationTitle(string oblast)
        => AlertsLocation.ContainsValue(oblast);

    public static int GetOblastIdByLocationTitle(string oblast)
        => AlertsLocation
            .FirstOrDefault(pair => EqualityComparer<string>.Default.Equals(pair.Value, oblast)).Key;

    public static IEnumerable<string> GetValidRegions(User user, bool flag)
        => flag
            ? AlertsLocation.Values
                .Where(region =>
                    user.Regions.Contains(region))
            : AlertsLocation.Values
                .Where(region =>
                    !user.Regions.Contains(region));

    public static List<BindingAlerts> GetBidingAlerts(AlertsModels.Alerts[] alerts)
    {
        List<BindingAlerts> bindingAlerts = new();

        int alertId = 1;
        
        foreach (var alert in alerts)
        {
            var bindingAlert = new BindingAlerts
            {
                MyAlertId = alertId,
                LocationTitle = alert.LocationTitle,
                LocationType = AlertsHelper.GetLocationTypeString(alert.LocationType),
                StartedAt = alert.StartedAt,
                UpdatedAt = alert.UpdatedAt,
                AlertType = AlertsHelper.GetAlertTypeString(alert.AlertType),
                LocationOblast = alert.LocationOblast,
                //LocationRaion = alert.LocationRaion is not null ? alert.LocationTitle : "Порожньо" 
                LocationRaion = alert.LocationRaion??= "Порожньо"
            };

            bindingAlerts.Add(bindingAlert);

            ++alertId;
        }

        return bindingAlerts;
    }

    public static string GetLocationTypeString(LocationType locationType)
    {
        return locationType switch
        {
            LocationType.Oblast => "Область",
            LocationType.Raion =>"Район",
            LocationType.City => "Місто",
            LocationType.Hromada => "Громада",
            _ => "Невідомо"
        };
    }
    
    public static string GetAlertTypeString(AlertType alertType)
    {
        return alertType switch
        {
            AlertType.AirRaid => "Повітряна тривога",
            AlertType.ArtilleryShelling => "Загроза артобстрілу",
            AlertType.UrbanFights => "Вуличні бої",
            AlertType.Chemical => "Хімічна небезпека",
            _ => "Ядерна небезпека"
        };
    }
}
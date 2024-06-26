﻿using Alerts.WPF.AlertsModels;
using Alerts.WPF.AlertsModels.Enums;
using Alerts.WPF.Data.Models;

namespace Alerts.WPF.Hepler;

public static class AlertsHelper
{
    private static readonly Dictionary<int, string> _alertsLocation;

    static AlertsHelper()
    {
        _alertsLocation = new Dictionary<int, string>()
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
        => _alertsLocation.ContainsKey(oblastId);

    public static bool IsValidLocationTitle(string oblast)
        => _alertsLocation.ContainsValue(oblast);

    public static int GetOblastIdByLocationTitle(string oblast)
        => _alertsLocation
            .FirstOrDefault(pair => EqualityComparer<string>.Default.Equals(pair.Value, oblast)).Key;

    public static IEnumerable<string> GetValidRegions(User user, bool flag)
        => flag
            ? _alertsLocation.Values
                .Where(region =>
                    user.Regions.Contains(region))
            : _alertsLocation.Values
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
                LocationType = GetLocationTypeString(alert.LocationType),
                StartedAt = alert.StartedAt,
                Duration = (DateTime.Now - alert.StartedAt).TimeSpanConvertString(),
                UpdatedAt = alert.UpdatedAt,
                AlertType = GetAlertTypeString(alert.AlertType),
                LocationOblast = alert.LocationOblast,
                LocationRaion = alert.LocationRaion??= "Відсутня інформація"
            };

            bindingAlerts.Add(bindingAlert);

            ++alertId;
        }

        return bindingAlerts;
    }

    private static string GetLocationTypeString(LocationType locationType)
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
    
    private static string GetAlertTypeString(AlertType alertType)
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
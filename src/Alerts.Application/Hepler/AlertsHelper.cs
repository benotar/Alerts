namespace Alerts.Application.Hepler;

public static class AlertsHelper
{
    private static readonly List<int> _allLocationsId;

    static AlertsHelper()
    {
        _allLocationsId = new List<int>();
        
        FillAllLocationsId();
    }
    
    public static bool IsValidLocationId(int locationId)
    {
        return _allLocationsId.Contains(locationId);
    }
    
    private static void FillAllLocationsId()
    {
        for (int i = 3; i < 32; i++)
        {
            if (i != 6 && i != 7)
            {
                _allLocationsId.Add(i);
            }
        }
    }
    
}
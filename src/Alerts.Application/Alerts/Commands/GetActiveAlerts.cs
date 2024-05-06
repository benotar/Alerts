using Alerts.Domain.Entities.AlertsEntities;
using Newtonsoft.Json;

namespace Alerts.Application.Alerts.Commands;

public class GetActiveAlerts
{
    private readonly string _url;
    
    public GetActiveAlerts(string url)
    {
        _url = url;
    }
    
    public async Task<RootObject> GetAlertsAsync()
    {
        using (HttpClient httpClient = new())
        {
            var request = await httpClient.GetAsync(_url);
        
            var content = await request.Content.ReadAsStringAsync();
        
            RootObject? result = JsonConvert.DeserializeObject<RootObject>(content);
            
            var convertAlertsDate = result.Alerts.Select(alert =>
            {
                alert.StartedAt = TimeZoneInfo.ConvertTimeFromUtc(alert.StartedAt, TimeZoneInfo.Local);
            
                alert.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(alert.UpdatedAt, TimeZoneInfo.Local);

                if (alert.FinishedAt.HasValue)
                {
                    alert.FinishedAt = TimeZoneInfo.ConvertTimeFromUtc(alert.FinishedAt.Value, TimeZoneInfo.Local);
                }

                return alert;
            
            }).ToArray();

            result.Alerts = convertAlertsDate;
            
            return result;
        }
    }
}
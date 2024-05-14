using Newtonsoft.Json;

namespace Alerts.WPF.AlertsModels;

public class Meta
{
    [JsonProperty("last_updated_at")]
    public DateTime LastUpdatedAt { get; set; }
    
    [JsonProperty("type")]
    public string Type { get; set; }
}
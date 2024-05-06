using Alerts.Domain.Enums;
using Newtonsoft.Json;

namespace Alerts.Domain.Entities.AlertsEntities;

public class Alerts
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("location_title")]
    public string LocationTitle { get; set; }
    
    [JsonProperty("location_type")]
    public LocationType LocationType { get; set; }
    
    [JsonProperty("started_at")]
    // [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime StartedAt { get; set; }
    
    [JsonProperty("finished_at")]
    // [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? FinishedAt { get; set; }
    
    [JsonProperty("updated_at")]
    // [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime UpdatedAt { get; set; }
    
    [JsonProperty("alert_type")]
    public AlertType AlertType { get; set; }
    
    [JsonProperty("location_oblast")]
    public string LocationOblast { get; set; }
    
    [JsonProperty("location_uid")]
    public int? LocationUid { get; set; }
    
    [JsonProperty("notes")]
    public string Notes { get; set; }
    
    [JsonProperty("country")]
    public object? Country { get; set; }
    
    [JsonProperty("calculated")]
    public bool? Calculated { get; set; }
    
    [JsonProperty("location_oblast_uid")]
    public int? LocationOblastUid { get; set; }
    
    [JsonProperty("location_raion")]
    public string? LocationRaion { get; set; } 
}
using System.Runtime.Serialization;

namespace Alerts.WPF.AlertsModels.Enums;

public enum AlertType
{
    [EnumMember(Value = "air_raid")]
    AirRaid,
    [EnumMember(Value = "artillery_shelling")]
    ArtilleryShelling, 
    [EnumMember(Value = "urban_fights")]
    UrbanFights, 
    [EnumMember(Value = "chemical")]
    Chemical, 
    [EnumMember(Value = "nuclear ")]
    Nuclear
}
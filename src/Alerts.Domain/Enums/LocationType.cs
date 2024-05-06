using System.Runtime.Serialization;

namespace Alerts.Domain.Enums;

public enum LocationType
{
    [EnumMember(Value = "oblast")]
    Oblast,
    [EnumMember(Value = "raion")]
    Raion, 
    [EnumMember(Value = "city")]
    City, 
    [EnumMember(Value = "hromada")]
    Hromada, 
    [EnumMember(Value = "unknown ")]
    Unknown
}
﻿using Newtonsoft.Json;

namespace Alerts.Domain.Entities.AlertsEntities;

public class RootObject
{
    [JsonProperty("alerts")]
    public Alerts[] Alerts { get; set; }
    
    [JsonProperty("meta")]
    public Meta Meta { get; set; }
    
    [JsonProperty("disclaimer")]
    public string Disclaimer { get; set; }
}
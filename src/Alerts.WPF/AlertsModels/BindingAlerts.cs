namespace Alerts.WPF.AlertsModels;

public class BindingAlerts
{
    public int MyAlertId { get; set; }
    public string LocationTitle { get; set; }
    
    public string LocationType { get; set; }
    
    public DateTime StartedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public string AlertType { get; set; }
    
    public string LocationOblast { get; set; }
    
    public string? LocationRaion { get; set; } 
}
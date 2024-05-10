namespace Alerts.WPF.Models;

public class User
{
    public long Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public string UserName { get; set; }
    
    public byte[] PasswordSalt { get; set; }
    
    public byte[] PasswordHash { get; set; }
    
    public string? Name { get; set; }
    
    public string Role { get; set; }
    
    public List<string> Regions { get; set; } = new();
}
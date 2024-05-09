namespace Alerts.Domain.Entities.Database;

public class User : DatabaseEntity
{
    public string UserName { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public string? Name { get; set; }
    public string Role { get; set; }
    public List<string> Regions { get; set; } = new();
}
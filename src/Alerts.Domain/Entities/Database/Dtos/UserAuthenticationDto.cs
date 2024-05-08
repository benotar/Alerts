namespace Alerts.Domain.Entities.Database.Dtos;

public class UserAuthenticationDto : DatabaseEntity
{
    public string UserName { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public string? Name { get; set; }
    public string Role { get; set; }
}
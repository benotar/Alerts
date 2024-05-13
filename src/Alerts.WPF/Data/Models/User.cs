using System.ComponentModel.DataAnnotations.Schema;

namespace Alerts.WPF.Data.Models;

public class User
{
    [Column("id")]
    public long Id { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    [Column("user_name")]
    public string UserName { get; set; }
    
    [Column("password_salt")]
    public byte[] PasswordSalt { get; set; }
    
    [Column("password_hash")]
    public byte[] PasswordHash { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
    
    [Column("role")]
    public string Role { get; set; }
    
    [Column("regions")]
    public List<string> Regions { get; set; } = new();
}
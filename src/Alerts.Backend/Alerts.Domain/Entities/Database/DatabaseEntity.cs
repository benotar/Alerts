using System.ComponentModel.DataAnnotations;

namespace Alerts.Domain.Entities.Database;

public abstract class DatabaseEntity
{
    [Key]
    public long Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
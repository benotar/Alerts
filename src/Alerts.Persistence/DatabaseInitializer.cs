namespace Alerts.Persistence;

public class DatabaseInitializer
{
    public static void Initialize(ApplicationDbContext db)
    {
        db.Database.EnsureCreated();
    }
}
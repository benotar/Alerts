namespace Alerts.Persistence;

public static class DatabaseInitializer
{
    public static void Initialize(ApplicationDbContext db)
    {
        db.Database.EnsureCreated();
    }
}
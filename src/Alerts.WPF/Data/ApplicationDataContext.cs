using Alerts.WPF.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Alerts.WPF.Data;

public class ApplicationDataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string dbPath = @"M:\IT Step and Programming\Coursework\Alerts\src\Alerts.Backend\Alerts.WebApp\alerts.db";
        //const string dbPath = @"..\..\..\src\Alerts.Backend\Alerts.WebApp\alerts.db";


        optionsBuilder.UseSqlite(
            $"Data Source={dbPath}");
    }
}
using Alerts.WPF.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Alerts.WPF.Data;

public class DataContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string path = @"M:\IT Step and Programming\Coursework\Alerts\src\Alerts.Backend\Alerts.WebApp\alerts.db";

        optionsBuilder.UseSqlite(
            $"Data Source={path}");
    }
}
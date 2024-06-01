using System.IO;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Hepler;
using Microsoft.EntityFrameworkCore;

namespace Alerts.WPF.Data;

public class ApplicationDataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var executionDirectory = AppDomain.CurrentDomain.BaseDirectory;

        var projectRoot = Path.GetFullPath(Path.Combine(executionDirectory, @"..\..\..\..\.."));
        
        var dbPath = Path.Combine(projectRoot, DatabaseHelper.GetRelativeDbPath());
        
        optionsBuilder.UseSqlite(
            $"Data Source={dbPath}");
    }
}
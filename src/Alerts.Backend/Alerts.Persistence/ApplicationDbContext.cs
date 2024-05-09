using Alerts.Application.Interfaces;
using Alerts.Domain.Entities.Database;
using Alerts.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Alerts.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options) ,IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
    }
}
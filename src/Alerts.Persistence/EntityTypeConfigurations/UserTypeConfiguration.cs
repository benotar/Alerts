using Alerts.Domain.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alerts.Persistence.EntityTypeConfigurations;

public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.UserName).HasColumnName("user_name");
        builder.Property(u => u.Name).HasColumnName("name");
        builder.Property(u => u.PasswordSalt).HasColumnName("password_salt");
        builder.Property(u => u.PasswordHash).HasColumnName("password_hash");
        builder.Property(u => u.CreatedAt).HasColumnName("created_at");
        builder.Property(u => u.UpdatedAt).HasColumnName("updated_at");
        builder.Property(u => u.Regions).HasColumnName("regions");
        builder.Property(u => u.Role).HasColumnName("role");
    }
}
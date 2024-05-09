using Alerts.Domain.Entities.Database;
using Microsoft.EntityFrameworkCore;

namespace Alerts.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
}
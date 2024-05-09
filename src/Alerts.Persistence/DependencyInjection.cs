using Alerts.Application.Configurations;
using Alerts.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alerts.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        var scope = services.BuildServiceProvider().CreateScope();
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite("Data source=alerts.db");
        });
        
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
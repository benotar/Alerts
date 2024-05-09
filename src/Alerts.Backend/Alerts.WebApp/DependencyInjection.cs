using Alerts.Application.Configurations;
using Microsoft.Extensions.Options;

namespace Alerts.WebApp;

public static class DependencyInjection
{
   public static void AddCustomConfiguration(this WebApplicationBuilder builder)
   {
      builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(JwtConfiguration.ConfigurationKey));

      builder.Services.AddSingleton(resolver =>
         resolver.GetRequiredService<IOptions<JwtConfiguration>>().Value);
   }
}
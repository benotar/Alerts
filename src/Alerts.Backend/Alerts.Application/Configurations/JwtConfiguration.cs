namespace Alerts.Application.Configurations;

public class JwtConfiguration
{
    public static readonly string ConfigurationKey = "Jwt";
    
    public string SecretKey { get; set; }
}
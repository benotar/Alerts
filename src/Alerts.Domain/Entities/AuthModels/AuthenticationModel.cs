namespace Alerts.Domain.Entities.AuthModels;

public class AuthenticationModel : IAuthModel
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
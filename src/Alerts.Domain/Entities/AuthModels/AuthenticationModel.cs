namespace Alerts.Domain.Entities.AuthModels;

public class AuthenticationModel : BaseAuthModel
{
    public string ConfirmPassword { get; set; }
    public string Name { get; set; }
}
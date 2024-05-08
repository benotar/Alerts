namespace Alerts.Domain.Entities.AuthModels;

public class AuthorizationModel : IAuthModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
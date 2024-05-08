namespace Alerts.Domain.Entities.AuthModels;

public abstract class BaseAuthModel
{
    public string UserName { get; set; }

    public string Password { get; set; }
}
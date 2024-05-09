namespace Alerts.Domain.Entities.AuthModels;

public interface IAuthModel
{
    public string UserName { get; set; }

    public string Password { get; set; }
}
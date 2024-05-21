using Alerts.WPF.Data;
using Alerts.WPF.Data.Models;

namespace Alerts.WPF.Hepler;

public static class UserHelper
{
    public static User GetActualUserData(ApplicationDataContext db, long userId)
    {
        return db.Users.Where(u => u.Id == userId).FirstOrDefault();
    }
}
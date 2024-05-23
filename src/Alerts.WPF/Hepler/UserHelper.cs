using Alerts.WPF.Data;
using Alerts.WPF.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Alerts.WPF.Hepler;

public static class UserHelper
{
    public static async Task<User?> GetActualUserData(string userName)
    {
        await using var db = new ApplicationDataContext();
        
        var actualUser = await db.Users
            .Where(u => u.UserName == userName)
            .FirstOrDefaultAsync();
            
        return actualUser;
    }
}
using System.Security.Cryptography;
using System.Text;
using Alerts.Domain.Entities.Database;

namespace Alerts.Application.Hepler;

public static class AuthHelper
{
    public static bool CheckPassword(string authModelPassword, User user)
    {
        var result = false;

        using (HMACSHA256 hmac = new(user.PasswordSalt))
        {
            var compute = hmac.ComputeHash(Encoding.UTF8.GetBytes(authModelPassword));

            result = compute.SequenceEqual(user.PasswordHash);
        }

        return result;
    }
}
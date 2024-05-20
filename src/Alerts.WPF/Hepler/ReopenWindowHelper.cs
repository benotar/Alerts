using System.Windows;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Windows;

namespace Alerts.WPF.Hepler;

public static class ReopenWindowHelper
{
    public static void ReOpenMainWindow(User user, string token, Window windowClose)
    {
        var mainContentWindow = new MainContentWindow(user, token);
        
        windowClose.Close();
        
        mainContentWindow.Show();
    }
}
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

    public static void ReOpenLoginWindow(Window windowClose)
    {
        var loginContentWindow = new LoginWindow();
        
        windowClose.Close();
        
        loginContentWindow.Show();
    }

    public static void ReOpenFullAlertWindow(FullAlertWindow fullAlertWindow, Window windowClose)
    {
        var newFullAlertWindow = new FullAlertWindow(fullAlertWindow.Token, fullAlertWindow.User, fullAlertWindow.WindowFlag);
        
        windowClose.Close();
        
        newFullAlertWindow.Show();
    }
}
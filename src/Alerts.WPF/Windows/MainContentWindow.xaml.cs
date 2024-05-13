using System.Windows;
using Alerts.WPF.Data.Models;

namespace Alerts.WPF.Windows;

public partial class MainContentWindow : Window
{
    private readonly UserModel _user;
    
    public MainContentWindow(UserModel user)
    {
        _user= user;
        
        InitializeComponent();
    }

    private void Load(object sender, RoutedEventArgs e)
    {
        HelloUserLabel.Content += _user.UserName;
    }
}
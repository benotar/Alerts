using System.Windows;
using Alerts.WPF.Controls;
using Alerts.WPF.Data.Models;
using MaterialDesignThemes.Wpf;

namespace Alerts.WPF.Windows;

public partial class MainContentWindow : Window
{
    private readonly User _user;
    
    public MainContentWindow(User user)
    {
        _user= user;
        
        InitializeComponent();
    }

    private void Load(object sender, RoutedEventArgs e)
    {
        HelloUserLabel.Content += _user.UserName;
        
        UserControl userControl = new(_user);

        MainPanel.Children.Add(userControl);
    }
    
    private void MainExitPopupBoxBtnOnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Alerts.WPF.Controls;
using Alerts.WPF.Data.Models;
using MaterialDesignThemes.Wpf;

namespace Alerts.WPF.Windows;

public partial class MainContentWindow : Window
{
    private User _user { get; set; }

    public MainContentWindow(User user)
    {
        InitializeComponent();

        _user = user;

        DataContext = this;
    }

    private void Load(object sender, RoutedEventArgs e)
    {
        HelloUserLabel.Content += _user?.UserName;

        AddUserControl();

        AddLabelSelectedRegion();
    }

    private void MainExitPopupBoxBtnOnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void AddUserControl()
    {
        MyUserControl userControl = new(_user);

        MainPanel.Children.Add(userControl);

        userControl.Margin = new Thickness(0, 0, 0, 15);
    }

    private void AddLabelSelectedRegion()
    {
        Label regionLabel = new();

        regionLabel.Content = "ТУТ БУДЕ ВИБРАНИЙ РЕГІОН";
        
        regionLabel.Style = FindResource("LabelStyle") as Style;
        regionLabel.Foreground = Brushes.White;

        MainPanel.Children.Add(regionLabel);
    }
    
}
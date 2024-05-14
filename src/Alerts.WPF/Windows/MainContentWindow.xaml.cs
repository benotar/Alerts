using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Alerts.WPF.Controls;
using Alerts.WPF.Data.Models;

namespace Alerts.WPF.Windows;

public partial class MainContentWindow : Window
{
    private User _user { get; set; }

    public Label SelectedRegionLabel;

    private Label _regionLabel;
    
    private MyUserControl _userControl;

    private StackPanel _selectedRegionStackPanel;
    
    private Button _getAlertForRegionButton;

    private AlertsControl _alertsControl;
    
    public MainContentWindow(User user)
    {
        InitializeComponent();

        _user = user;

        SelectedRegionLabel = new Label();

        _regionLabel = new Label();
        
        _selectedRegionStackPanel = new StackPanel();
        
        _userControl = new MyUserControl(this, _user);

        _getAlertForRegionButton = new Button();

        _alertsControl = new AlertsControl(new AlertsModels.Alerts
        {
            LocationTitle = _user.Regions.FirstOrDefault(r => r == "Дніпропетровсьddка область"),
            StartedAt = DateTime.Now,
        });
    }

    private void Load(object sender, RoutedEventArgs e)
    {
        HelloUserLabel.Content += _user.UserName;
        
        AddUserControl();

        AddSelectedRegionStackPanel();

        AddAlertsControl();
        
        AddGetAlertForRegionButton();
        
    }
    private void MainExitPopupBoxBtnOnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    private void AddUserControl()
    {
        MainPanel.Children.Add(_userControl);

        _userControl.Margin = new Thickness(15, 0, 0, 15);
    }

    private void AddAlertsControl()
    {
        _alertsControl.Margin = new Thickness(0, 15, 0, 0);
        
        MainPanel.Children.Add(_alertsControl);
    }
    
    private void AddSelectedRegionLabel()
    {
        SelectedRegionLabel.Content = "Порожньо";
        
        SelectedRegionLabel.Style = FindResource("LabelStyle") as Style;
        
        _selectedRegionStackPanel.Children.Add(SelectedRegionLabel);
    }
    private void AddRegionLabel()
    {
        _regionLabel.Style = FindResource("LabelStyle") as Style;
        
        _regionLabel.Content = "Вибраний регіон:";
        
        _regionLabel.Margin = new Thickness(15, 0, 15, 0);

        _selectedRegionStackPanel.Children.Add(_regionLabel);
    }
    private void AddSelectedRegionStackPanel()
    {
        _selectedRegionStackPanel.Orientation = Orientation.Horizontal;

        AddRegionLabel();
        
        AddSelectedRegionLabel();

        MainPanel.Children.Add(_selectedRegionStackPanel);
    }

    private void AddGetAlertForRegionButton()
    {
        _getAlertForRegionButton.Style = FindResource("MaterialDesignOutlinedSecondaryButton") as Style;

        _getAlertForRegionButton.Width = 270;
        
        _getAlertForRegionButton.Content = "Отримати актуальну інформацію";

        _getAlertForRegionButton.HorizontalAlignment = HorizontalAlignment.Center;

        _getAlertForRegionButton.Margin = new Thickness(0, 170, 0, 0);

        MainPanel.Children.Add(_getAlertForRegionButton);
    }
    
}
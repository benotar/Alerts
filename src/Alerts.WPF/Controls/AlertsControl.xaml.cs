using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Hepler;

namespace Alerts.WPF.Controls;

public partial class AlertsControl : UserControl
{
    private readonly AlertsModels.Alerts _alert;
    public AlertsControl(AlertsModels.Alerts alert)
    {
        InitializeComponent();
        
        _alert = alert;
    }

    private void Load(object sender, RoutedEventArgs e)
    {
        if (!AlertsHelper.IsActiveAlert(_alert, _alert.LocationTitle))
        {
            IsActiveAlertLabel.Content = "Не активна";
        }
    }
}
using System.Windows;
using System.Windows.Input;
using Alerts.WPF.AlertsModels;
using Alerts.WPF.Hepler;

namespace Alerts.WPF.Windows;

public partial class FullAlertSecondWindow : Window
{
    private readonly BindingAlerts _selectedLocation;
    
    private readonly FullAlertWindow _fullAlertWindow;

    public FullAlertSecondWindow(BindingAlerts selectedLocation, FullAlertWindow fullAlertWindow)
    {
        InitializeComponent();
        
        _selectedLocation = selectedLocation;
        
        _fullAlertWindow = fullAlertWindow;

        DataContext = _selectedLocation;
    }

    private async void FullAlertSecondWindowLoad(object sender, RoutedEventArgs e)
    {
        FullInfoBackBtn.IsEnabled = false;

        await Task.Delay(5000);
        
        FullInfoBackBtn.IsEnabled = true;

    }
    
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        DragMove();
    }
    
    private void FullInfoBackBtnOnClick(object sender, RoutedEventArgs e)
    {
        ReOpenFullAlertWindow();
    }
    
    private void ReOpenFullAlertWindow()
    {
        ReopenWindowHelper.ReOpenFullAlertWindow(_fullAlertWindow, this);
    }
    
}
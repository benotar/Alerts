using System.Windows;
using Alerts.WPF.Controls;

namespace Alerts.WPF.Windows;

public partial class FullAlertWindow : Window
{
    private readonly MainContentWindow _mainContentWindow;
    
    private readonly string _token;
    
    public FullAlertWindow(MainContentWindow mainContentWindow, string token)
    {
        InitializeComponent();
        
        _mainContentWindow = mainContentWindow;
        
        _token = token;
    }

    private void LoadFullAlertControl()
    {
        var fullAlertControl = new FullAlertControl(this, _token);

        Content = fullAlertControl;
    }
}
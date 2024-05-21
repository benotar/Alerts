using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.HttpQueries;
using Alerts.WPF.Windows;

namespace Alerts.WPF.Controls;

public partial class FullAlertControl : UserControl
{
    private readonly MyHttpClient _httpClient;
    
    private readonly FullAlertWindow _fullAlertWindow;
    
    private readonly string _token;

    public FullAlertControl(FullAlertWindow fullAlertWindow, string token)
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();
        
        _fullAlertWindow = fullAlertWindow;
        
        _token = token;
    }
    
    
    private void Unload(object sender, RoutedEventArgs e)
    {
        _httpClient.Dispose();
    }
    
    
    
    private void ReOpenMainWindow()
    {
        //ReopenWindowHelper.ReOpenMainWindow(_user, _token, _addRegionWindow, _db); // TODO потім добавити це все
    }
}
using System.Windows;
using Alerts.WPF.AlertsModels;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Hepler;
using Alerts.WPF.HttpQueries;

namespace Alerts.WPF.Windows;

public partial class FullAlertWindow : Window
{
    private readonly string _token;
    
    private readonly User _user;

    private AlertsModels.Alerts?[] _alerts;

    private readonly MyHttpClient _httpClient;

    public List<BindingAlerts> BindingAlerts { get; set; }
    
    public FullAlertWindow( string token, User user)
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();
        
        _token = token;
        
        _user = user;

        InitializeDataAsync();
    }

    private async void InitializeDataAsync()
    {
        await SetAlertsAsync();

        SetDataForBinding();
    }
    
    private void Unload(object sender, RoutedEventArgs e)
    {
        _httpClient.Dispose();
    }
    
    private async Task SetAlertsAsync()
    {
        const string apiUrl = "https://localhost:44305/alertsApi/GetActiveAlerts";

        var rootAlerts = await _httpClient.GetAsync<RootObject>(apiUrl);

        _alerts = rootAlerts.Alerts;
    }

    private void SetDataForBinding()
    {
        BindingAlerts = AlertsHelper.ConvertForBiding(_alerts);

        if (BindingAlerts is null)
        {
            MessageBox.Show("Не вдалося конвертувати тривоги");
        }
        else
        {
            DataContext = this;
        }
    }
    
    private void ReOpenMainWindow()
    {
        ReopenWindowHelper.ReOpenMainWindow(_user, _token, this);
    }
}
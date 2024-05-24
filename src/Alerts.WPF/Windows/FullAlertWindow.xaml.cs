using System.Windows;
using Alerts.WPF.AlertsModels;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Hepler;
using Alerts.WPF.HttpQueries;

namespace Alerts.WPF.Windows;

public partial class FullAlertWindow : Window
{
    private readonly Window _windowFlag;
    
    private readonly string _token;
    
    private readonly User _user;

    private AlertsModels.Alerts?[] _alerts;

    private readonly MyHttpClient _httpClient;

    public List<BindingAlerts> BindingAlerts { get; set; }
    
    public FullAlertWindow( string token, User user, Window windowFlag)
    {
        InitializeComponent();

        _windowFlag = windowFlag;
        
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
    
    private void FullInfoBtnOnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void FullInfoCancelBtnOnClick(object sender, RoutedEventArgs e)
    {
        switch (_windowFlag)
        {
            case MainContentWindow : ReOpenMainWindow();
                break;
            case LoginWindow : ReOpenLoginWindow();
                break;
        }
    }
    
    private async Task SetAlertsAsync()
    {
        const string apiUrl = "https://localhost:44305/alertsApi/GetActiveAlerts";

        var rootAlerts = await _httpClient.GetAsync<RootObject>(apiUrl);

        _alerts = rootAlerts.Alerts;
    }
    
    private void SetDataForBinding()
    {
        BindingAlerts = AlertsHelper.GetBidingAlerts(_alerts);

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

    private void ReOpenLoginWindow()
    {
        ReopenWindowHelper.ReOpenLoginWindow(this);
    }
}
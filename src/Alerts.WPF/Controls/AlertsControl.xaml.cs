using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Hepler;
using Alerts.WPF.HttpQueries;

namespace Alerts.WPF.Controls;

public partial class AlertsControl : UserControl
{
    public int _oblastId { get; set; }

    private readonly string _token;

    private MyHttpClient _httpClient;

    public AlertsControl(int oblastId, string token)
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();
        
        _oblastId = oblastId;

        _token = TrimCharToken(token);
    }

    private void Load(object sender, RoutedEventArgs e)
    {
        GetActualData();
    }

    public async void GetActualData()
    {
        IsActiveAlertLabel.Content = null;

        if (!AlertsHelper.IsValidOblastId(_oblastId))
        {
            MessageBox.Show("Invalid oblast");

            return;
        }

        var url = $"https://localhost:44305/alertsApi/GetAlertByOblast?locationId={_oblastId}";
        
        var content = await _httpClient.GetWithTokenAsync<string>(url, _token);
        
        content = content.Trim('"');

        switch (content)
        {
            case "N":
                IsActiveAlertLabel.Content = "Немає інформації про повітряну тривогу";
                return;
            case "P":
                IsActiveAlertLabel.Content = "Часткова тривога в районах чи громадах";
                return;
        }

        IsActiveAlertLabel.Content = "Повітряна тривога активна в усій області";
    }

    private void Unload(object sender, RoutedEventArgs e)
    {
        _httpClient.Dispose();
    }
    
    private static string TrimCharToken(string token)
    {
        return token.Trim('"');
    }
}
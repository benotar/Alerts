using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Hepler;
using Alerts.WPF.HttpQueries;

namespace Alerts.WPF.Controls;

public partial class AlertsControl : UserControl
{
    public int OblastId { get; set; }

    private readonly string _token;

    private readonly MyHttpClient _httpClient;

    public AlertsControl(int oblastId, string token)
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();
        
        OblastId = oblastId;

        _token = TrimCharToken(token);
    }

    private void Load(object sender, RoutedEventArgs e)
    {
        GetActualData();
    }

    public async void GetActualData()
    {
        IsActiveAlertLabel.Content = null;

        if (!AlertsHelper.IsValidOblastId(OblastId))
        {
            MessageBox.Show("Invalid oblast");

            return;
        }

        var content = (await _httpClient.GetWithTokenAsync<string>(ApiUrls.GetActiveAlertsByOblast(OblastId), _token)).Trim('"');

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
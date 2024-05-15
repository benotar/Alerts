using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Hepler;

namespace Alerts.WPF.Controls;

public partial class AlertsControl : UserControl
{
    public int _oblastId { get; set; }

    public readonly string _token;

    public AlertsControl(int oblastId, string token)
    {
        InitializeComponent();

        _oblastId = oblastId;

        _token = TrimCharToken(token);
    }

    private async void Load(object sender, RoutedEventArgs e)
    {
        // IsActiveAlertLabel.Content = null;
        //
        // if (!AlertsHelper.IsValidOblastId(_oblastId))
        // {
        //     MessageBox.Show("Invalid oblast");
        //
        //     return;
        // }
        //
        // var url = $"https://localhost:44305/alertsApi/GetAlertByOblast?locationId={_oblastId}";
        //
        // using (var httpClient = new HttpClient())
        // {
        //     httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        //
        //     var response = await httpClient.GetAsync(url);
        //
        //     var content = await response.Content.ReadAsStringAsync();
        //
        //     switch (content)
        //     {
        //         case "N":
        //             IsActiveAlertLabel.Content = "Немає інформації про тримогу";
        //             return;
        //         case "P":
        //             IsActiveAlertLabel.Content = "Часткова тривога в районах чи громадах";
        //             return;
        //     }
        //
        //     IsActiveAlertLabel.Content = "Повітряна тривога активна в усій області";
        // }

        RefreshData();
    }

    public async void RefreshData()
    {
        IsActiveAlertLabel.Content = null;
        
        if (!AlertsHelper.IsValidOblastId(_oblastId))
        {
            MessageBox.Show("Invalid oblast");

            return;
        }

        var url = $"https://localhost:44305/alertsApi/GetAlertByOblast?locationId={_oblastId}";

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                MessageBox.Show($"Error :{response.StatusCode}");

                return;
            }
            
            var content = await response.Content.ReadAsStringAsync();

            content = content.Trim('"');

            switch (content)
            {
                case "N":
                    IsActiveAlertLabel.Content = "Немає немає інформації про повітряну тривогу";
                    return;
                case "P":
                    IsActiveAlertLabel.Content = "Часткова тривога в районах чи громадах";
                    return;
            }

            IsActiveAlertLabel.Content = "Повітряна тривога активна в усій області";
        }
    }

    private static string TrimCharToken(string token)
    {
        return token.Trim('"');
    }
    
}
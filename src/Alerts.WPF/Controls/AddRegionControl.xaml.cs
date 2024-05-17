using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.HttpQueries;
using Alerts.WPF.Windows;

namespace Alerts.WPF.Controls;

public partial class AddRegionControl : UserControl
{
    private readonly MyHttpClient _httpClient;

    private readonly AddRegionWindow _addRegionWindow;

    private readonly MyUserControl _userControl;
    
    private readonly string _token;

    private readonly long _userId;
    
    public AddRegionControl(AddRegionWindow addRegionWindow, MyUserControl userControl,string token, long userId)
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();

        _addRegionWindow = addRegionWindow;

        _userControl = userControl;
        
        _token = token;

        _userId = userId;
    }

    private async void AddRegionBtnOnClick(object sender, RoutedEventArgs e)
    {
        var region = RegionTextBox.Text;

        var apiUrl = $"https://localhost:44305/userApi/AddRegion/{_userId}/{region}";

        var result = await _httpClient.PutWithTokenAsync(apiUrl, _token);

        if (!result)
        {
            return;
        }

        MessageBox.Show($"Регіон \'{region}\' успішно додано!");

        _userControl.Fill();
        
        _addRegionWindow.Close();
    }

    private void AddRegionCancelBtnOnClick(object sender, RoutedEventArgs e)
    {
       _addRegionWindow.Close();
    }

    private void Unload(object sender, RoutedEventArgs e)
    {
        _httpClient.Dispose();
    }
}
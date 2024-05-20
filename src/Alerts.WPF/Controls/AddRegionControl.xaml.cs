using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Hepler;
using Alerts.WPF.HttpQueries;
using Alerts.WPF.Windows;

namespace Alerts.WPF.Controls;

public partial class AddRegionControl : UserControl
{
    private readonly MyHttpClient _httpClient;

    private readonly MyUserControl _userControl;

    private readonly AddRegionWindow _addRegionWindow;
    
    private readonly string _token;
    
    private readonly User _user;
    
    public AddRegionControl(AddRegionWindow addRegionWindow, MyUserControl userControl,string token, User user)
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();
        
        _userControl = userControl;

        _addRegionWindow = addRegionWindow;
        
        _token = token;

        _user = user;
    }

    private async void AddRegionBtnOnClick(object sender, RoutedEventArgs e)
    {
        var region = RegionTextBox.Text;

        var apiUrl = $"https://localhost:44305/userApi/AddRegion/{_user.Id}/{region}";

        var result = await _httpClient.PutWithTokenAsync(apiUrl, _token);

        if (!result)
        {
            return;
        }

        _user.Regions.Add(region);
        
        MessageBox.Show($"Регіон \'{region}\' успішно додано!");

        _userControl.FillRegionsComboBox();

        ReOpenMainWindow();
    }

    private void AddRegionCancelBtnOnClick(object sender, RoutedEventArgs e)
    {
        ReOpenMainWindow();
    }

    private void Unload(object sender, RoutedEventArgs e)
    {
        _httpClient.Dispose();
    }

    private void AddRegionsControlLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            MessageBox.Show("Не вдалось отримати список доступних регіонів!");
            
            return;
        }

        var validRegions = AlertsHelper.AlertsLocation.Values
            .Where(region =>
                !_user.Regions.Contains(region));

        foreach (var region in validRegions)
        {
            comboBox.Items.Add(region);
        }
    }
    
    private void ReOpenMainWindow()
    {
        _addRegionWindow.Close();
        
        var mainContentWindow = new MainContentWindow(_user, _token);
        
        mainContentWindow.Show();
    }
}
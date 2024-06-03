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

    private User _user;

    public AddRegionControl(AddRegionWindow addRegionWindow, MyUserControl userControl, string token, User user)
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();

        _userControl = userControl;

        _addRegionWindow = addRegionWindow;

        _token = token;

        _user = user;
    }

    private async void AddRegionBtnOnClickAsync(object sender, RoutedEventArgs e)
    {
        var region = RegionTextBox.Text;

        var result = await _httpClient.PutWithTokenAsync(ApiUrls.GetAddRegionUrl(_user.Id, region), _token);

        if (!result)
        {
            return;
        }
        
        MessageBox.Show($"Регіон \'{region}\' успішно додано!");

        _userControl.FillRegionsComboBoxAsync();

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

    private async void AddRegionsControlLoadAsync(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            MessageBox.Show("Не вдалось отримати список доступних регіонів!");
            
            return;
        }
        
        var actualUser = await UserHelper.GetActualUserDataAsync(_user.UserName);

        if (actualUser is null)
        {
            MessageBox.Show("Не вдалось отримати користувача!");

            return;
        }

        _user = actualUser;

        foreach (var region in AlertsHelper.GetValidRegions(_user, false))
        {
            comboBox.Items.Add(region);
        }
    }

    private void ReOpenMainWindow()
    {
        ReopenWindowHelper.ReOpenMainWindow(_user, _token, _addRegionWindow);
    }
}
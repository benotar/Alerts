using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Hepler;
using Alerts.WPF.HttpQueries;
using Alerts.WPF.Windows;

namespace Alerts.WPF.Controls;

public partial class DeleteRegionControl : UserControl
{
    private readonly MyHttpClient _httpClient;
    
    private readonly DeleteRegionWindow _deleteRegionWindow;
    
    private readonly MyUserControl _userControl;

    private readonly string _token;
    
    private User _user;
    
    public DeleteRegionControl(DeleteRegionWindow deleteRegionWindow, MyUserControl userControl, string token, User user)
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();

        _deleteRegionWindow = deleteRegionWindow;
        
        _userControl = userControl;

        _token = token;

        _user = user;
    }
    
    private async void DeleteRegionBtnOnClick(object sender, RoutedEventArgs e)
    {
        var region = RegionTextBox.Text;

        //var apiUrl = $"https://localhost:44305/userApi/DeleteRegion/{_user.Id}/{region}";

        var result = await _httpClient.DeleteWithTokenAsync(ApiUrls.GetDeleteRegionUrl(_user.Id, region), _token);

        if (!result)
        {
            return;
        }
        
        MessageBox.Show($"Регіон \'{region}\' успішно видалено!");

        _userControl.FillRegionsComboBox();
        
        ReOpenMainWindow();
    }
    
    private async void DeleteRegionsControlLoad(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            MessageBox.Show("Не вдалось отримати список доступних регіонів!");
            
            return;
        }
        
        var actualUser = await UserHelper.GetActualUserData(_user.UserName);

        if (actualUser is null)
        {
            MessageBox.Show("Не вдалось отримати користувача!");

            return;
        }

        _user = actualUser;
        
        foreach (var region in AlertsHelper.GetValidRegions(_user, true))
        {
            comboBox.Items.Add(region);
        }
    }
    
    private void Unload(object sender, RoutedEventArgs e)
    {
        _httpClient.Dispose();
    }
    
    private void AddRegionCancelBtnOnClick(object sender, RoutedEventArgs e)
    {
        ReOpenMainWindow();
    }
    
    private void ReOpenMainWindow()
    {
        ReopenWindowHelper.ReOpenMainWindow(_user, _token, _deleteRegionWindow);
    }
}
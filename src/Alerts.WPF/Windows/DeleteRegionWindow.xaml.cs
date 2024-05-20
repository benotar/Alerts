using System.Windows;
using Alerts.WPF.Controls;
using Alerts.WPF.Data.Models;
using Alerts.WPF.HttpQueries;

namespace Alerts.WPF.Windows;

public partial class DeleteRegionWindow : Window
{
    private readonly MyUserControl _userControl;
    
    private readonly string _token;
    
    private readonly User _user;
    
    public DeleteRegionWindow(MyUserControl userControl, string token, User user)
    {
        InitializeComponent();

        _userControl = userControl;
        _token = token;

        _user = user;
        
        DeleteAddRegionControl();
    }

    private void DeleteAddRegionControl()
    {
        var deleteRegionControl = new DeleteRegionControl(this, _userControl, _token, _user);

        Content = deleteRegionControl;
    }
}
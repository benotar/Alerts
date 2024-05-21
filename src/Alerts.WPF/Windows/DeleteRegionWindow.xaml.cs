using System.Windows;
using Alerts.WPF.Controls;
using Alerts.WPF.Data;
using Alerts.WPF.Data.Models;
using Alerts.WPF.HttpQueries;

namespace Alerts.WPF.Windows;

public partial class DeleteRegionWindow : Window
{
    private readonly MyUserControl _userControl;
    
    private readonly string _token;
    
    private readonly User _user;
    
    private readonly ApplicationDataContext _db;

    public DeleteRegionWindow(MyUserControl userControl, string token, User user, ApplicationDataContext db)
    {
        InitializeComponent();

        _userControl = userControl;
        
        _token = token;

        _user = user;
        
        _db = db;

        LoadDeleteRegionControl();
    }

    private void LoadDeleteRegionControl()
    {
        var deleteRegionControl = new DeleteRegionControl(this, _userControl, _token, _user, _db);

        Content = deleteRegionControl;
    }
}
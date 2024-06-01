using System.Windows;
using System.Windows.Input;
using Alerts.WPF.Controls;
using Alerts.WPF.Data.Models;

namespace Alerts.WPF.Windows;

public partial class AddRegionWindow : Window
{
    private readonly MyUserControl _userControl;
    
    private readonly string _token;
    
    private readonly User _user;
    
    public AddRegionWindow(MyUserControl userControl, string token, User user)
    {
        InitializeComponent();
        
        _userControl = userControl;
        
        _token = token;
        
        _user = user;
        
        LoadAddRegionControl();
    }
    
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        DragMove();
    }
    
    private void LoadAddRegionControl()
    {
        var addRegionControl = new AddRegionControl(this, _userControl, _token, _user);

        Content = addRegionControl;
    }
}
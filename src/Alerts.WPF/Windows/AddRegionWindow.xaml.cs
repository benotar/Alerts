using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Controls;

namespace Alerts.WPF.Windows;

public partial class AddRegionWindow : System.Windows.Window
{
    private readonly MyUserControl _userControl;
    
    private readonly string _token;
    
    private readonly long _id;

    public AddRegionWindow(MyUserControl userControl,string token, long id)
    {
        InitializeComponent();

        _userControl = userControl;
        
        _token = token;
        
        _id = id;
    }
    
    private void Load(object sender, RoutedEventArgs e)
    {
        var addRegionControl = new AddRegionControl(this, _userControl, _token, _id);
        
        AddRegionWindowStackPanel.Children.Add(addRegionControl);
    }
}
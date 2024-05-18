using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Controls;

namespace Alerts.WPF.Windows;

public partial class AddRegionWindow : System.Windows.Window
{
    private readonly MainContentWindow _mainContentWindow;
    
    private readonly MyUserControl _userControl;
    
    private readonly string _token;
    
    private readonly long _id;

    public AddRegionWindow(MainContentWindow mainContentWindow, MyUserControl userControl,string token, long id)
    {
        InitializeComponent();

        _mainContentWindow = mainContentWindow;
        
        _userControl = userControl;
        
        _token = token;
        
        _id = id;
    }
    
    private void Load(object sender, RoutedEventArgs e)
    {
        var addRegionControl = new AddRegionControl(this,_mainContentWindow , _userControl, _token, _id);
        
        AddRegionWindowStackPanel.Children.Add(addRegionControl);
    }
}
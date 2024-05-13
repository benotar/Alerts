using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Data.Models;

namespace Alerts.WPF.Controls;

public partial class UserControl : System.Windows.Controls.UserControl
{
    private User? _user;
    
    public UserControl(User user)
    {
        InitializeComponent();
        
        _user = user;

        DataContext = _user;
    }

    private void FillRegionsComboBox(object sender, RoutedEventArgs e)
    {
        var comboBox = sender as ComboBox;

        if (comboBox is null || _user is null)
        {
            // TODO 
            MessageBox.Show("TEMP!");
        }
        
        foreach (var region in _user.Regions)
        {
            comboBox.Items.Add(region);
        }
    }
}
using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Data.Models;

namespace Alerts.WPF.Controls;

public partial class MyUserControl : System.Windows.Controls.UserControl
{
    private User _user { get; set; }
    
    public MyUserControl(User user)
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
            
            return;
        }
        
        foreach (var region in _user.Regions)
        {
            comboBox.Items.Add(region);
        }
    }
    
}
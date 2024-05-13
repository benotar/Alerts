using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Data.Models;

namespace Alerts.WPF.Controls;

public partial class MyUserControl : System.Windows.Controls.UserControl
{
    public User? User { get; set; }
    
    public MyUserControl(User user)
    {
        InitializeComponent();
        
        User = user;

        DataContext = User;
    }

    private void FillRegionsComboBox(object sender, RoutedEventArgs e)
    {
        var comboBox = sender as ComboBox;

        if (comboBox is null || User is null)
        {
            // TODO 
            MessageBox.Show("TEMP!");
            
            return;
        }
        
        foreach (var region in User.Regions)
        {
            comboBox.Items.Add(region);
        }
    }

    
    // TODO 
    private void RegionSelectedChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        
        if (comboBox is null || User is null)
        {
            // TODO 
            MessageBox.Show("TEMP!");
            
            return;
        }

        if (comboBox.SelectionBoxItem is not string selectionBoxItem)
        {
            // TODO 
            MessageBox.Show("Error!");

            return;
        }

        RegionControl regionControl = new(selectionBoxItem);

        UserControlPanel.Children.Add(regionControl);
    }
}
using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Windows;

namespace Alerts.WPF.Controls;

public partial class MyUserControl : System.Windows.Controls.UserControl
{
    private MainContentWindow _mainContentWindow;
    
    private User _user { get; set; }
    
    public MyUserControl(MainContentWindow mainContentWindow, User user)
    {
        InitializeComponent();

        _mainContentWindow = mainContentWindow;
        
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


    private void RegionsComboBoxItemSelected(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;

        if (comboBox is null)
        {
            // TODO
            MessageBox.Show("Не вдалось отримати комбо бокс");
            
            return;
        }

        var selectedRegion = comboBox.SelectedItem.ToString();

        if (string.IsNullOrEmpty(selectedRegion))
        {
            // TODO
            MessageBox.Show("Не вдалось отримати регіон");
            
            return;
        }
        
        _mainContentWindow.SelectedRegionLabel.Content = selectedRegion;
    }
}
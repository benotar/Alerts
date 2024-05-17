using System.Windows;
using System.Windows.Controls;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Windows;

namespace Alerts.WPF.Controls;

public partial class MyUserControl : System.Windows.Controls.UserControl
{
    private MainContentWindow _mainContentWindow;

    private User _user;
    
    public MyUserControl(MainContentWindow mainContentWindow, User user)
    {
        InitializeComponent();

        _mainContentWindow = mainContentWindow;
        
        _user = user;
        
        DataContext = _user;
    }

    private void FillRegionsComboBox(object sender, RoutedEventArgs e)
    {
        // var comboBox = sender as ComboBox;
        //
        // if (comboBox is null)
        // {
        //     MessageBox.Show("Не вдалось отримати регіони!");
        //     
        //     return;
        // }
        //
        // comboBox.Items.Clear();
        //
        // foreach (var region in _user.Regions)
        // {
        //     comboBox.Items.Add(region);
        // }

        Fill();
    }


    private void RegionsComboBoxItemSelected(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        
        if (comboBox is null)
        {
            MessageBox.Show("Не вдалось отримати комбо бокс");
            
            return;
        }
        
        var selectedRegion = comboBox.SelectedItem.ToString();
        
        if (string.IsNullOrEmpty(selectedRegion))
        {
            MessageBox.Show("Не вдалось отримати регіон");
            
            return;
        }

        if (string.IsNullOrEmpty(selectedRegion))
        {
            _mainContentWindow.SelectedRegionLabel.Content = "Порожньо";
        }
        _mainContentWindow.SelectedRegionLabel.Content = RegionsComboBox.SelectedItem;
    }

    
    // TODO 
    public void Fill()
    {
        RegionsComboBox.Items.Clear();
        
        if (RegionsComboBox is null)
        {
            MessageBox.Show("Не вдалось отримати регіони!");
            
            return;
        }
        
        foreach (var region in _user.Regions)
        {
            RegionsComboBox.Items.Add(region);
        }
    }
}
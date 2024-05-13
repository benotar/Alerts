using System.Windows;
using System.Windows.Controls;

namespace Alerts.WPF.Controls;

public partial class RegionControl : UserControl
{
    public string Region { get; set; }
    
    public RegionControl(string region)
    {
        InitializeComponent();

        Region = region;
        
        DataContext = Region;
    }


    private void RegionControlLoad(object sender, RoutedEventArgs e)
    {
        RegionNameLabel.Content = Region;
    }
}
using System.Windows;

namespace Alerts.WPF.Windows;

public partial class MainContentWindow : Window
{
    private readonly string _temp;
    
    public MainContentWindow(string temp)
    {
        _temp = temp;
        
        InitializeComponent();
    }

    private void Load(object sender, RoutedEventArgs e)
    {
        TempLabel.Content = _temp;
    }
}
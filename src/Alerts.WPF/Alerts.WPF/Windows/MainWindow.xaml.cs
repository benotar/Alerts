using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Alerts.WPF.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public bool IsDarkTheme { get; set; }

    private readonly PaletteHelper _paletteHelper;
    
    public MainWindow()
    {
        _paletteHelper = new PaletteHelper();
        
        InitializeComponent();
    }

    private void ToggleTheme(object sender, RoutedEventArgs e)
    {
        ITheme theme = _paletteHelper.GetTheme();

        IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark;
        
        theme.SetBaseTheme(IsDarkTheme ? Theme.Light : Theme.Dark);
        
        _paletteHelper.SetTheme(theme);
    } 

    private void ExitPopupBoxBtnOnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void LoginBtnOnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void CreateAccountBtnOnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        
        DragMove();
    }
}
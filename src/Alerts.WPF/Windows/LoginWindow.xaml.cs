using System.Windows;
using System.Windows.Input;
using Alerts.WPF.Data;
using Alerts.WPF.HttpQueries;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace Alerts.WPF.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    private readonly ApplicationDataContext _db;
    private bool IsDarkTheme { get; set; }

    private readonly PaletteHelper _paletteHelper;

    private MyHttpClient _httpClient;

    public LoginWindow()
    {
        InitializeComponent();

        _db = new ApplicationDataContext();

        _paletteHelper = new PaletteHelper();

        _httpClient = new MyHttpClient();
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
        System.Windows.Application.Current.Shutdown();
    }

    private async void LoginBtnOnClick(object sender, RoutedEventArgs e)
    {
        var userName = UserNameTxtBox.Text;

        var userPassword = UserPasswordBox.Password;

        const string apiUrl = "https://localhost:44305/auth/login";
        
        var token = await _httpClient.PostAsync<string>(apiUrl, new { UserName = userName, Password = userPassword });

        if (IsNullOrEmpty(token))
        {
            
        }

        _httpClient.Dispose();
        
        var user = await _db.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

        MainContentWindow mainContentWindow = new(user!, token);

        mainContentWindow.Show();
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
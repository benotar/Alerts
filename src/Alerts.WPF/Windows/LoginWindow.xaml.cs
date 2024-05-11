using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Alerts.WPF.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    
    
    public bool IsDarkTheme { get; set; }

    private readonly PaletteHelper _paletteHelper;

    public LoginWindow()
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
        System.Windows.Application.Current.Shutdown();
    }

    private async void LoginBtnOnClick(object sender, RoutedEventArgs e)
    {
        var userName = UserNameTxtBox.Text;

        var userPassword = UserPasswordBox.Password;

        using (var httpClient = new HttpClient())
        {
            string apiUrl = "https://localhost:44305/auth/login";

            var json = JsonSerializer.Serialize(new { UserName = userName, Password = userPassword });
            
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);



            MainContentWindow mainContentWindow = new(await response.Content.ReadAsStringAsync());

            mainContentWindow.ShowDialog();
        }
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
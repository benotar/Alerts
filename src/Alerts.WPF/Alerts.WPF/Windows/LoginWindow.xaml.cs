using System.Net.Http;
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
        Application.Current.Shutdown();
    }

    private async void LoginBtnOnClick(object sender, RoutedEventArgs e)
    {
        var userName = UserNameTxtBox.Text;

        var userPassword = UserPasswordBox.Password;

        string responseBody = await Temp(userName, userPassword);

        MainContentWindow mainContentWindow = new(responseBody);

        mainContentWindow.ShowDialog();
    }

    private void CreateAccountBtnOnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    // TODO Temp

    private async Task<string> Temp(string a, string b)
    {
        string apiUrl = "https://localhost:44305/auth/login";

        string jsonBody = $"{{\"userName\": \"{a}\", \"password\": \"{b}\"}}";

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, new StringContent(jsonBody));


            string responseBody = String.Empty;

            if (response.IsSuccessStatusCode)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                MessageBox.Show($"Error: {response.StatusCode}");
            }

            return responseBody;
        }
    }


    
    
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        DragMove();
    }
}
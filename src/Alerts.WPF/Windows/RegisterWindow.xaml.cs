using System.Windows;
using System.Windows.Input;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Hepler;
using Alerts.WPF.HttpQueries;
using static System.String;

namespace Alerts.WPF.Windows;

public partial class RegisterWindow : Window
{
    private readonly MyHttpClient _httpClient;
    
    public RegisterWindow()
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();
    }

    private async void CreateAccountBtnOnClickAsync(object sender, RoutedEventArgs e)
    {

        if (IsNullOrEmpty(CreateLoginTextBlock.Text)
            || IsNullOrEmpty(CreateNameTextBlock.Text)
            || IsNullOrEmpty(CreateUserPasswordBox.Password)
            || IsNullOrEmpty(ConfirmUserPasswordBox.Password))
        {
            MessageBox.Show("Відсутні дані про нового користувача!");
            
            return;
        }
        
        var userModel = new RegisterModel
        {
            UserName = CreateLoginTextBlock.Text,
            Name = CreateNameTextBlock.Text,
            Password = CreateUserPasswordBox.Password,
            ConfirmPassword = ConfirmUserPasswordBox.Password
        };

        var newUser = await _httpClient.PostAsync<User>(ApiUrls.GetRegisterUrl(), userModel);

        if (newUser is null)
        {
            MessageBox.Show("Не вдалося створити користувача!");
            
            return;
        }
        
        MessageBox.Show($"Користувач \'{newUser.UserName}\' успішно створений!");

        ReOpenLoginWindow();
    }
    
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        DragMove();
    }

    private void CreateAccountExitBtnOnClick(object sender, RoutedEventArgs e)
    {
        ReOpenLoginWindow();
    }

    private void ReOpenLoginWindow()
    {
        ReopenWindowHelper.ReOpenLoginWindow(this);
    }
}
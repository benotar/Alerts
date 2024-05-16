using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using Alerts.WPF.Data;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Hepler;
using Alerts.WPF.HttpQueries;

namespace Alerts.WPF.Windows;

public partial class RegisterWindow : Window
{
    private readonly ApplicationDataContext _db;

    private MyHttpClient _httpClient;
    
    public RegisterWindow(ApplicationDataContext db)
    {
        InitializeComponent();

        _httpClient = new MyHttpClient();
        
        _db = db;
    }

    private async void CreateAccountBtnOnClick(object sender, RoutedEventArgs e)
    {
        var userModel = new RegisterModel
        {
            UserName = CreateLoginTextBlock.Text,
            Name = CreateNameTextBlock.Text,
            Password = CreateUserPasswordBox.Password,
            ConfirmPassword = ConfirmUserPasswordBox.Password
        };

        const string apiUrl = "https://localhost:44305/auth/Register";

        var newUser = await _httpClient.PostAsync<User>(apiUrl, userModel);

        if (newUser is null)
        {
            MessageBox.Show("Не владося створити користувача!");
            
            return;
        }
        
        MessageBox.Show($"Користувач {newUser.UserName} успішно створений!");

        this.Close();
    }
    
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        DragMove();
    }
}
﻿using System.Windows;
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
        
        SetDarkTheme();

        _httpClient = new MyHttpClient();
    }

    private void SetDarkTheme()
    {
        var theme = _paletteHelper.GetTheme();

        theme.SetBaseTheme(Theme.Dark);

        _paletteHelper.SetTheme(theme);
    }

    private async void LoginBtnOnClick(object sender, RoutedEventArgs e)
    {
        var userName = UserNameTxtBox.Text;

        var userPassword = UserPasswordBox.Password;

        if (IsNullOrEmpty(userName) || IsNullOrEmpty(userPassword))
        {
            MessageBox.Show("Відсутні дані про користувача!");

            return;
        }

        const string apiUrl = "https://localhost:44305/auth/login";

        var token = await _httpClient.PostAsync<string>(apiUrl, new { UserName = userName, Password = userPassword });

        if (token is null)
        {
            return;
        }

        var user = await _db.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

        MainContentWindow mainContentWindow = new(user!, token);

        mainContentWindow.Show();
    }

    private void CreateAccountBtnOnClick(object sender, RoutedEventArgs e)
    {
        var registerWindow = new RegisterWindow();

        registerWindow.Show();
    }


    private void ExitPopupBoxBtnOnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        DragMove();
    }


    private void Unload(object sender, RoutedEventArgs e)
    {
        _httpClient.Dispose();
    }
}
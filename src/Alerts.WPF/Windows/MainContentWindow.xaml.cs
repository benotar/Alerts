﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Alerts.WPF.Controls;
using Alerts.WPF.Data.Models;
using Alerts.WPF.Hepler;

namespace Alerts.WPF.Windows;

public partial class MainContentWindow : Window
{
    private readonly User _user;
    
    private readonly string _token;

    public Label SelectedRegionLabel;

    private Label _regionLabel;
    
    private MyUserControl _userControl;

    private StackPanel _selectedRegionStackPanel;
    
    private Button _getAlertForRegionButton;

    private AlertsControl _alertsControl;
    

    public MainContentWindow(User user, string token)
    {
        InitializeComponent();

        _user = user;

        _token = token;
        
        SelectedRegionLabel = new Label();

        _regionLabel = new Label();
        
        _selectedRegionStackPanel = new StackPanel();

        _userControl = new MyUserControl(this, _user);

        _getAlertForRegionButton = new Button();
        
    }

    private async void LoadAsync(object sender, RoutedEventArgs e)
    {
        HelloUserLabel.Content += _user.UserName;
        
        AddUserControl();

        AddSelectedRegionStackPanel();
        
        AddGetAlertForRegionButton();
        
        AllInfoForAirAlertBtn.IsEnabled = false;

        await Task.Delay(5000);

        AllInfoForAirAlertBtn.IsEnabled = true;
    }
    private void MainExitPopupBoxBtnOnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    
    private async void GetAlertForRegionButtonOnClickAsync(object sender, RoutedEventArgs e)
    {
        var location = SelectedRegionLabel.Content.ToString();

        if (!AlertsHelper.IsValidLocationTitle(location))
        {
            MessageBox.Show("Вказаний некоректний регіон");
            
            return;
        }
        
        AddAlertControl(location);

        _getAlertForRegionButton.IsEnabled = false;

        await Task.Delay(5000);

        _getAlertForRegionButton.IsEnabled = true;
    }
    
    private void AllInfoForAirAlertBtnOnClick(object sender, RoutedEventArgs e)
    {
        OpenFullAlertWindow();
    }
    
    private void AddUserRegionPopupBoxBtnOnClick(object sender, RoutedEventArgs e)
    {
        OpenAddRegionWindow();
    }

    private void DeleteUserRegionPopupBoxBtnOnClick(object sender, RoutedEventArgs e)
    {
        OpenDeleteRegionWindow();
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        DragMove();
    }
    
    private void OpenFullAlertWindow()
    {
        var fullAlertWindow = new FullAlertWindow(_token, _user, this);
        
        this.Close();

        fullAlertWindow.Show();
    }
    
    private void OpenAddRegionWindow()
    {
        var addRegionWindow = new AddRegionWindow(_userControl, _token, _user);
        
        this.Close();
        
        addRegionWindow.Show();
    }
    
    private void OpenDeleteRegionWindow()
    {
        var deleteRegionWindow = new DeleteRegionWindow(_userControl, _token, _user);
        
        this.Close();
        
        deleteRegionWindow.Show();
    }
    
    private void AddAlertControl(string location)
    {
        var oblastId = AlertsHelper.GetOblastIdByLocationTitle(location);
        
        var existingControl = MainPanel.Children.OfType<AlertsControl>()
            .FirstOrDefault();

        if (existingControl is null)
        {
            _alertsControl = new AlertsControl(oblastId, _token)
            {
                Margin = new Thickness(0, 15, 0, 0)
            };

            MainPanel.Children.Add(_alertsControl);
            
            return;
        }

        existingControl.OblastId = oblastId;
        
        existingControl.GetActualDataAsync();
    }
    
    private void AddUserControl()
    {
        MainPanel.Children.Add(_userControl);

        _userControl.Margin = new Thickness(15, 0, 0, 15);
    }
    
    private void AddSelectedRegionLabel()
    {
        SelectedRegionLabel.Content = "Порожньо";
        
        SelectedRegionLabel.Style = FindResource("LabelStyle") as Style;
        
        _selectedRegionStackPanel.Children.Add(SelectedRegionLabel);
    }
    
    private void AddRegionLabel()
    {
        _regionLabel.Style = FindResource("LabelStyle") as Style;
        
        _regionLabel.Content = "Вибраний регіон:";
        
        _regionLabel.Margin = new Thickness(15, 0, 15, 0);

        _selectedRegionStackPanel.Children.Add(_regionLabel);
    }
    
    private void AddSelectedRegionStackPanel()
    {
        _selectedRegionStackPanel.Orientation = Orientation.Horizontal;

        AddRegionLabel();
        
        AddSelectedRegionLabel();

        MainPanel.Children.Add(_selectedRegionStackPanel);
    }

    private void AddGetAlertForRegionButton()
    {
        _getAlertForRegionButton.Style = FindResource("MaterialDesignOutlinedSecondaryButton") as Style;

        _getAlertForRegionButton.Width = 255;
        
        _getAlertForRegionButton.Content = "Отримати актуальну інформацію";

        _getAlertForRegionButton.HorizontalAlignment = HorizontalAlignment.Right;

        _getAlertForRegionButton.VerticalAlignment = VerticalAlignment.Center;

        _getAlertForRegionButton.Margin = new Thickness(0, -33, 12, 0);

        _getAlertForRegionButton.Click += GetAlertForRegionButtonOnClickAsync;

        MainPanel.Children.Add(_getAlertForRegionButton);
    }
}
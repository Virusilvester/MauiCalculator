using MauiCalculator.Services;
using Microsoft.Maui.Storage;

namespace MauiCalculator.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        LoadTheme();
    }

    private void OnThemeChanged(object sender, EventArgs e)
    {
        var selected = ThemePicker.SelectedIndex;

        AppTheme theme = selected switch
        {
            1 => AppTheme.Light,
            2 => AppTheme.Dark,
            _ => AppTheme.Unspecified
        };

        ThemeService.SetTheme(theme);
        Preferences.Set("AppTheme", selected);
    }

    private void LoadTheme()
    {
        var savedTheme = Preferences.Get("AppTheme", 0);
        ThemePicker.SelectedIndex = savedTheme;
    }
}

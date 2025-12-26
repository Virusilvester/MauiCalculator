namespace MauiCalculator.Services;

public static class ThemeService
{
    public static void SetTheme(AppTheme theme)
    {
        Application.Current!.UserAppTheme = theme;
    }
}

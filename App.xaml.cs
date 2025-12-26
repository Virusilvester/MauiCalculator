namespace MauiCalculator;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		var savedTheme = Preferences.Get("AppTheme", 0);

        Application.Current!.UserAppTheme = savedTheme switch
        {
            1 => AppTheme.Light,
            2 => AppTheme.Dark,
            _ => AppTheme.Unspecified
        };
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}
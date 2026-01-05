namespace GorselProgOdevFinal.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        if (Application.Current.UserAppTheme == AppTheme.Dark)
        {
            ThemeSwitch.IsToggled = true;
        }
    }

    private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }

    private void LogoutButton_Clicked(object sender, EventArgs e)
    {

        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}
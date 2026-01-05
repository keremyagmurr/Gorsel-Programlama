using GorselProgOdevFinal.ViewModels;

namespace GorselProgOdevFinal.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}
using GorselProgOdevFinal.ViewModels;

namespace GorselProgOdevFinal.Views;

public partial class WeatherPage : ContentPage
{
    public WeatherPage()
    {
        InitializeComponent();
        BindingContext = new WeatherViewModel();
    }
}
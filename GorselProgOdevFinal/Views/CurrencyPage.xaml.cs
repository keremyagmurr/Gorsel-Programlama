using GorselProgOdevFinal.ViewModels;

namespace GorselProgOdevFinal.Views;

public partial class CurrencyPage : ContentPage
{
    public CurrencyPage()
    {
        InitializeComponent();
        BindingContext = new CurrencyViewModel();
    }
}
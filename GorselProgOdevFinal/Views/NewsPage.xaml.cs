using GorselProgOdevFinal.ViewModels;

namespace GorselProgOdevFinal.Views;

public partial class NewsPage : ContentPage
{
    public NewsPage()
    {
        InitializeComponent();
        var vm = new NewsViewModel();
        BindingContext = vm;

        vm.LoadNewsCommand.Execute("Manþet");
    }
}
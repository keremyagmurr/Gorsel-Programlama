using GorselProgOdevFinal.ViewModels;

namespace GorselProgOdevFinal.Views;

public partial class TodoPage : ContentPage
{
    private TodoViewModel _viewModel;

    public TodoPage()
    {
        InitializeComponent();
        _viewModel = new TodoViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadTodosCommand.Execute(null);
    }
}
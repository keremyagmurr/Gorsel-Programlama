using GorselProgOdevFinal.Models;
using GorselProgOdevFinal.Services;
using GorselProgOdevFinal.ViewModels;

namespace GorselProgOdevFinal.Views;

public partial class TodoDetailPage : ContentPage
{
    private TodoItem _todoItem;
    private readonly TodoService _service;

    public TodoDetailPage(TodoItem item)
    {
        InitializeComponent();
        _service = new TodoService();
        _todoItem = item;

        if (_todoItem != null)
        {
            Title = "Görevi Düzenle";
            TitleEntry.Text = _todoItem.Title;
            DetailEntry.Text = _todoItem.Detail;
            DatePickerControl.Date = _todoItem.Date.Date;
            TimePickerControl.Time = _todoItem.Date.TimeOfDay;
        }
        else
        {
            Title = "Yeni Görev Ekle";
            DatePickerControl.Date = DateTime.Now;
            TimePickerControl.Time = DateTime.Now.TimeOfDay;
        }
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DisplayAlert("Hata", "Lütfen bir baþlýk girin.", "Tamam");
            return;
        }

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        try
        {
            DateTime fullDate = DatePickerControl.Date.Add(TimePickerControl.Time);

            if (_todoItem == null)
            {
                var newItem = new TodoItem
                {
                    Title = TitleEntry.Text,
                    Detail = DetailEntry.Text,
                    Date = fullDate,
                    IsDone = false
                };
                await _service.AddTodoAsync(newItem);
            }
            else
            {
                _todoItem.Title = TitleEntry.Text;
                _todoItem.Detail = DetailEntry.Text;
                _todoItem.Date = fullDate;

                await _service.UpdateTodoAsync(_todoItem);
            }

            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Kaydedilemedi: " + ex.Message, "Tamam");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }
}
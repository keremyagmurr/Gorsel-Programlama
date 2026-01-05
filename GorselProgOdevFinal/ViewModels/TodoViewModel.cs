using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GorselProgOdevFinal.Models;
using GorselProgOdevFinal.Services;
using GorselProgOdevFinal.Views;
using System.Collections.ObjectModel;

namespace GorselProgOdevFinal.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        private readonly TodoService _service;

        public TodoViewModel()
        {
            _service = new TodoService();
            LoadTodos();
        }

        [ObservableProperty]
        ObservableCollection<TodoItem> todos;

        [ObservableProperty]
        bool isBusy;

        [RelayCommand]
        public async Task LoadTodos()
        {
            IsBusy = true;
            try
            {
                var list = await _service.GetTodosAsync();
                var sortedList = list.OrderByDescending(x => x.Date).ToList();
                Todos = new ObservableCollection<TodoItem>(sortedList);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Hata", "Veriler yüklenemedi: " + ex.Message, "Tamam");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GoToDetail(TodoItem item)
        {
            if (item == null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new TodoDetailPage(null));
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new TodoDetailPage(item));
            }
        }
        [RelayCommand]
        public async Task DeleteTodo(TodoItem item)
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Silinsin mi?", "Bu görevi silmeyi onaylıyor musunuz?", "Evet", "Hayır");
            if (answer)
            {
                await _service.DeleteTodoAsync(item.Id);
                Todos.Remove(item);
            }
        }

        [RelayCommand]
        public async Task ToggleDone(TodoItem item)
        {
            if (item != null)
            {
                await _service.UpdateTodoAsync(item);

                var index = Todos.IndexOf(item);
                if (index >= 0)
                {
                    Todos[index] = item; 
                }
            }
        }
    }
}
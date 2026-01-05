using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Firebase.Database;
using Firebase.Database.Query;
using GorselProgOdevFinal.Helpers;
using GorselProgOdevFinal.Models;

namespace GorselProgOdevFinal.Services
{
    public class TodoService
    {
        private readonly FirebaseClient _client;

        public TodoService()
        {
            _client = new FirebaseClient(Constants.FirebaseBaseUrl);
        }

        public async Task<List<TodoItem>> GetTodosAsync()
        {
            var items = await _client
                .Child("Todos") 
                .OnceAsync<TodoItem>();

            return items.Select(item => new TodoItem
            {
                Id = item.Key,
                Title = item.Object.Title,
                Detail = item.Object.Detail,
                Date = item.Object.Date,
                IsDone = item.Object.IsDone
            }).ToList();
        }

        public async Task AddTodoAsync(TodoItem item)
        {
            await _client
                .Child("Todos")
                .PostAsync(item);
        }

        public async Task UpdateTodoAsync(TodoItem item)
        {
            await _client
                .Child("Todos")
                .Child(item.Id)
                .PutAsync(item);
        }

        public async Task DeleteTodoAsync(string id)
        {
            await _client
                .Child("Todos")
                .Child(id)
                .DeleteAsync();
        }
    }
}
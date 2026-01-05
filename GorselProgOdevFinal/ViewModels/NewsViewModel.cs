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
    public partial class NewsViewModel : ObservableObject
    {
        private readonly NewsService _service;
        private Dictionary<string, string> categories = new()
        {
            { "Manşet", "https://www.trthaber.com/manset_articles.rss" },
            { "Gündem", "https://www.trthaber.com/gundem_articles.rss" },
            { "Ekonomi", "https://www.trthaber.com/ekonomi_articles.rss" },
            { "Spor", "https://www.trthaber.com/spor_articles.rss" },
            { "Bilim", "https://www.trthaber.com/bilim_teknoloji_articles.rss" }
        };

        public NewsViewModel()
        {
            _service = new NewsService();
            CurrentCategory = "Manşet";
        }

        [ObservableProperty]
        ObservableCollection<NewsItem> newsList;

        [ObservableProperty]
        string currentCategory;

        [ObservableProperty]
        bool isBusy;

        [RelayCommand]
        public async Task LoadNews(string categoryName)
        {
            if (IsBusy) return;
            IsBusy = true;
            CurrentCategory = categoryName;

            try
            {
                string rssUrl = categories[categoryName];
                var items = await _service.GetNewsAsync(rssUrl);
                NewsList = new ObservableCollection<NewsItem>(items);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task OpenDetail(NewsItem item)
        {
            if (item == null) return;
           
            await Application.Current.MainPage.Navigation.PushAsync(new NewsDetailPage(item));
        }
    }
}
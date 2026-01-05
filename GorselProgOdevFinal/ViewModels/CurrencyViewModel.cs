using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GorselProgOdevFinal.Models;
using GorselProgOdevFinal.Services;
using System.Collections.ObjectModel;

namespace GorselProgOdevFinal.ViewModels
{
    public partial class CurrencyViewModel : ObservableObject
    {
        private readonly CurrencyService _service;

        [ObservableProperty]
        ObservableCollection<Currency> currencies;

        [ObservableProperty]
        bool isBusy;

        public CurrencyViewModel()
        {
            _service = new CurrencyService();
            Currencies = new ObservableCollection<Currency>();
            LoadCurrencies(); 
        }

        [RelayCommand]
        public async Task LoadCurrencies()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                await Task.Delay(1500);

                var list = await _service.GetCurrenciesAsync();

                Currencies.Clear(); 

                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        Currencies.Add(item);
                    }
                }
                else
                {
                   await Application.Current.MainPage.DisplayAlert("Uyarı", "Veri çekilemedi. İnternet bağlantınızı kontrol edin.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Hata", ex.Message, "Tamam");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
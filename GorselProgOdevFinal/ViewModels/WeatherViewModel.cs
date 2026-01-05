using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GorselProgOdevFinal.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;

namespace GorselProgOdevFinal.ViewModels
{
    public partial class WeatherViewModel : ObservableObject
    {
        private string filePath = Path.Combine(FileSystem.AppDataDirectory, "saved_mgm_cities.json");

        [ObservableProperty]
        ObservableCollection<WeatherCity> cities;

        [ObservableProperty]
        ObservableCollection<string> availableCities;

        [ObservableProperty]
        string selectedCityName;

        public WeatherViewModel()
        {
            Cities = new ObservableCollection<WeatherCity>();

            AvailableCities = new ObservableCollection<string>
            {
                "ADANA", "ANKARA", "ANTALYA", "AYDIN", "BALIKESIR",
                "BARTIN", "BOLU", "BURSA", "CANAKKALE", "CORUM",
                "DENIZLI", "DIYARBAKIR", "EDIRNE", "ERZURUM", "ESKISEHIR",
                "GAZIANTEP", "HATAY", "ISTANBUL", "IZMIR", "KASTAMONU",
                "KAYSERI", "KOCAELI", "KONYA", "MALATYA", "MANISA",
                "MARDIN", "MERSIN", "MUGLA", "ORDU", "RIZE",
                "SAKARYA", "SAMSUN", "SIVAS", "SANLIURFA", "TEKIRDAG",
                "TRABZON", "VAN", "ZONGULDAK"
            };

            LoadCities();
        }
        [RelayCommand]
        public void AddCity()
        {
            if (string.IsNullOrEmpty(SelectedCityName)) return;

            if (Cities.Any(c => c.Name == SelectedCityName)) return;

            string normalized = NormalizeForMGM(SelectedCityName);

            var newCity = new WeatherCity
            {
                Name = SelectedCityName,
                NormalizedName = normalized
            };

            Cities.Add(newCity);
            SaveCities();
        }

        [RelayCommand]
        public void DeleteCity(WeatherCity city)
        {
            if (Cities.Contains(city))
            {
                Cities.Remove(city);
                SaveCities();
            }
        }

        private string NormalizeForMGM(string text)
        {
            string upper = text.ToUpper(new System.Globalization.CultureInfo("tr-TR"));

            return upper
                .Replace('Ç', 'C')
                .Replace('Ğ', 'G')
                .Replace('İ', 'I') 
                .Replace('Ö', 'O')
                .Replace('Ş', 'S')
                .Replace('Ü', 'U');
        }

        private void SaveCities()
        {
            try
            {
                var json = JsonConvert.SerializeObject(Cities);
                File.WriteAllText(filePath, json);
            }
            catch { }
        }

        private void LoadCities()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var json = File.ReadAllText(filePath);
                    var list = JsonConvert.DeserializeObject<List<WeatherCity>>(json);
                    if (list != null) foreach (var item in list) Cities.Add(item);
                }
                catch { }
            }
        }
    }
}
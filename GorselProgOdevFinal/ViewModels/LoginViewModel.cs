using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GorselProgOdevFinal.Services;

namespace GorselProgOdevFinal.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;

        public LoginViewModel()
        {
            _authService = new AuthService();
        }

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private bool isLoginMode = true;

        [ObservableProperty]
        private bool isBusy; 

        [RelayCommand]
        public void ToggleMode()
        {
            IsLoginMode = !IsLoginMode;
        }

        [RelayCommand]
        public async Task LoginOrRegister()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                string resultId = null;

                if (IsLoginMode)
                {
                    resultId = await _authService.LoginAsync(Email, Password);
                    if (!string.IsNullOrEmpty(resultId))
                    {
                        await Shell.Current.GoToAsync("//HomePage");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Hata", "Giriş başarısız. Bilgileri kontrol edin.", "Tamam");
                    }
                }
                else
                {
                    resultId = await _authService.RegisterAsync(Email, Password, Username);
                    if (!string.IsNullOrEmpty(resultId))
                    {
                        await Application.Current.MainPage.DisplayAlert("Başarılı", "Kayıt oluşturuldu, giriş yapılıyor...", "Tamam");
                        await Shell.Current.GoToAsync("//HomePage");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Hata", "Kayıt yapılamadı.", "Tamam");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Hata", "Geçersiz Giriş", "Tamam");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
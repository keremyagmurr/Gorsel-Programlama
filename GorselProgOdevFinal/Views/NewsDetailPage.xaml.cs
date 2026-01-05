using GorselProgOdevFinal.Models;

namespace GorselProgOdevFinal.Views;

public partial class NewsDetailPage : ContentPage
{
    private NewsItem _newsItem;

    public NewsDetailPage(NewsItem item)
    {
        InitializeComponent();
        _newsItem = item;
        BindingContext = _newsItem;
    }

    // Paylaþ Butonu
    private async void Share_Clicked(object sender, EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Uri = _newsItem.Link,
            Title = _newsItem.Title,
            Text = _newsItem.Title
        });
    }
    private async void OpenSource_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_newsItem.Link))
        {
            try
            {
                await Browser.Default.OpenAsync(_newsItem.Link, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Link açma hatasý: " + ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GorselProgOdevFinal.Models;
using Newtonsoft.Json;

namespace GorselProgOdevFinal.Services
{
    public class NewsService
    {
        private const string BaseApiUrl = "https://api.rss2json.com/v1/api.json?rss_url=";

        public async Task<List<NewsItem>> GetNewsAsync(string categoryUrl)
        {
            List<NewsItem> newsList = new List<NewsItem>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(BaseApiUrl + categoryUrl);

                    var root = JsonConvert.DeserializeObject<NewsRoot>(response);

                    if (root?.Status == "ok")
                    {
                        newsList = root.Items;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Haber hatası: " + ex.Message);
            }

            return newsList;
        }
    }
}
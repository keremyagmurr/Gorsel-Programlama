using System.Xml.Linq;
using GorselProgOdevFinal.Models;

namespace GorselProgOdevFinal.Services
{
    public class CurrencyService
    {
        private const string Url = "https://www.tcmb.gov.tr/kurlar/today.xml";

        public async Task<List<Currency>> GetCurrenciesAsync()
        {
            List<Currency> currencies = new List<Currency>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(Url);
                    XDocument doc = XDocument.Parse(response);

                    foreach (var item in doc.Descendants("Currency"))
                    {
                        string code = item.Attribute("CurrencyCode")?.Value;
                        string buying = item.Element("ForexBuying")?.Value;
                        string selling = item.Element("ForexSelling")?.Value;

                        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(selling)) continue;

                        currencies.Add(new Currency
                        {
                            Name = code,
                            Buying = buying,
                            Selling = selling,
                            ChangeRate = "%0.05", 
                            ColorCode = "Green",  
                            DirectionIcon = "▲"   
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Hata: " + ex.Message);
            }

            return currencies;
        }
    }
}
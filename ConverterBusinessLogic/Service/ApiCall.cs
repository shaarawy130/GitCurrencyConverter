using System.Net;
using ConverterBusinessLogic.Interfaces;
using ConverterBusinessLogic.Model;
using Newtonsoft.Json;

namespace ConverterBusinessLogic.Service
{
    public class ApiCall : IApiCall
    {
        public WebClient WebClient { get; set; }

        public CurrencyRates GetRates()
        {
            using (var w = new WebClient())
            {
                var jsonData = w.DownloadString("https://api.fixer.io/latest?base=EUR");

                if (!string.IsNullOrEmpty(jsonData))
                    return JsonConvert.DeserializeObject<CurrencyRates>(jsonData);
                return null;
            }
        }
    }


}
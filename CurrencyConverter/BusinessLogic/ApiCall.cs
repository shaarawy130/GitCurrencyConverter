using CurrencyConverter.BusinessLogic.Interfaces;

namespace CurrencyConverter.BusinessLogic
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using CurrencyConverter.Models;
    using Newtonsoft.Json;

    public class ApiCall : IApiCall
    {
       public WebClient WebClient { get; set; }

        public  CurrencyRates GetRates()
        {
            using (var w = new WebClient())
            {
                var jsonData =  w.DownloadString("https://api.fixer.io/latest?base=EUR");

                if (!string.IsNullOrEmpty(jsonData))
                    return JsonConvert.DeserializeObject<CurrencyRates>(jsonData);
                return null;
            }
        }
    }
}
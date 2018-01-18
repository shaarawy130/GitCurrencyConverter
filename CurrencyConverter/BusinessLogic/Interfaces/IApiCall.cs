using System.Net;
using System.Threading.Tasks;
using CurrencyConverter.Models;

namespace CurrencyConverter.BusinessLogic.Interfaces
{
    public interface IApiCall
    {
        WebClient WebClient { get; set; }
        CurrencyRates GetRates();
    }
}

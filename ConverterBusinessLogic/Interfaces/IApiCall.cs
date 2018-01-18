using System.Net;
using ConverterBusinessLogic.Model;

namespace ConverterBusinessLogic.Interfaces
{
    public interface IApiCall
    {
        WebClient WebClient { get; set; }
        CurrencyRates GetRates();
    }
}
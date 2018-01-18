namespace CurrencyConverter.ViewModels
{
    public class RateViewModel
    {
        public RateViewModel(string currency, decimal rateValue)
        {
            Currency = currency;
            RateValue = rateValue;
        }
        public string Currency { get; set; }
        public decimal RateValue { get; set; }
    }
}
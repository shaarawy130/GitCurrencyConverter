namespace ConverterPresentation.Models
{
    public class Rate
    {
        public Rate(string currency, decimal rateValue)
        {
            Currency = currency;
            RateValue = rateValue;
        }
        public string Currency { get; set; }
        public decimal RateValue { get; set; }
    }
}

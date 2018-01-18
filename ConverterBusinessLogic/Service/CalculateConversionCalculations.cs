using ConverterBusinessLogic.Interfaces;

namespace ConverterBusinessLogic.Service
{
    public class CalculateConversionCalculations : ICurrencyConverterCalculations
    {
        public decimal Convert(decimal currency, decimal targetCurrency, decimal amount)
        {
            return amount * targetCurrency / currency;
        }
    }
}
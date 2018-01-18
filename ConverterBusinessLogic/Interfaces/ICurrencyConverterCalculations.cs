namespace ConverterBusinessLogic.Interfaces
{
    public interface ICurrencyConverterCalculations
    {
        decimal Convert(decimal currency, decimal targetCurrency, decimal amount);
    }
}
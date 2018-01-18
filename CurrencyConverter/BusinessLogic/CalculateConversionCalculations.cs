using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.BusinessLogic.Interfaces;
using CurrencyConverter.Models;

namespace CurrencyConverter.BusinessLogic
{
    public class CalculateConversionCalculations : ICurrencyConverterCalculations
    {
       //// private static CalculateConversion _instance;

        ////public static ICurrencyConverter GetInstance()
        ////{
        ////    if (_instance == null)
        ////        _instance = new CalculateConversion();

        ////    return _instance;
        ////}

        public decimal Convert(decimal currency, decimal targetCurrency, decimal amount)
        {
            return amount * targetCurrency / currency;
        }
    }
}

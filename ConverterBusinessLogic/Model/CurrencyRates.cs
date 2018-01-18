using System.Collections.Generic;

namespace ConverterBusinessLogic.Model
{
    public class CurrencyRates
    {
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}

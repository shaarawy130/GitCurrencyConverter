using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPresentation.ViewModels
{
    class RateViewModel
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPresentation.Messages
{
    public class FromCurrencyChangedMessage
    {
        public decimal FromCurrencyValue { get; }

        public FromCurrencyChangedMessage(decimal fromCurrencyValue)
        {
            FromCurrencyValue = fromCurrencyValue;
        }
    }
}

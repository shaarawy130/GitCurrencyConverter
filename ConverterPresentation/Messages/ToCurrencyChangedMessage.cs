using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPresentation.Messages
{
   public class ToCurrencyChangedMessage
    {
        public decimal ToCurrencyValue { get; }

        public ToCurrencyChangedMessage(decimal toCurrencyValue)
        {
            ToCurrencyValue = toCurrencyValue;
        }
    }
}

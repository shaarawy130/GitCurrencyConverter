using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CurrencyConverter.ViewModels;

namespace CurrencyConverter
{
    class ValidationDouble : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double i;
            if (double.TryParse(value.ToString(), out i))
            {
                return new ValidationResult(true, null);
            }
            else
            {
             ////   new ConverterViewModel().SetEnabled(false);
                return new ValidationResult(false, "Please enter only numbers");
            }
        }
    }
}

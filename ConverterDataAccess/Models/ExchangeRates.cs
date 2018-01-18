using System;

namespace ConverterDataAccess.Models
{
    public class ExchangeRates
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateExtracted { get; set; }
        public virtual decimal ToEur { get; set; }
    }
}
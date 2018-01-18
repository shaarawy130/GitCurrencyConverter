using ConverterDataAccess.Models;
using FluentNHibernate.Mapping;

namespace ConverterDataAccess.Mapping
{
    public class ExchangeRateMap : ClassMap<ExchangeRates>
    {
        public ExchangeRateMap()
        {
            Id(r => r.Id);
            Map(r => r.Name);
            Map(r => r.DateExtracted);
            Map(r => r.ToEur);
        }
    }

}
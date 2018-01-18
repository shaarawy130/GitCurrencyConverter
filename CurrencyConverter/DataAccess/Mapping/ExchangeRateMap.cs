using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.DataAccess.Models;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;

namespace CurrencyConverter.DataAccess.Mapping
{
    class ExchangeRateMap : ClassMap<ExchangeRates>
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

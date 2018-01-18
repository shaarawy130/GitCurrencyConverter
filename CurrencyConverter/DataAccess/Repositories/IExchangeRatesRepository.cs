using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.DataAccess.Models;

namespace CurrencyConverter.DataAccess.Repositories
{
    public interface IExchangeRatesRepository
    {
        void Save(Dictionary<string, decimal> loadDataApi, DateTime date);
        List<ExchangeRates> Load();
        bool IsOutDated(int days);
    }
}
using System;
using System.Collections.Generic;
using ConverterDataAccess.Models;

namespace ConverterDataAccess.Repositories.Interfaces
{
    public interface IExchangeRatesRepository
    {
        List<ExchangeRates> Load();
        bool InitializeAndCheckEmptyOrOutdated();
        void SaveAndOverwrite(Dictionary<string, decimal> currenciesFromApi, DateTime date);
    }
}
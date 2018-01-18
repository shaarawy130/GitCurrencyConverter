using System;
using System.Collections.Generic;
using System.Linq;
using ConverterBusinessLogic.Interfaces;
using ConverterDataAccess.Repositories;
using ConverterDataAccess.Repositories.Interfaces;

namespace ConverterBusinessLogic.Service
{
    public class DataLoaderAndSaver : IDataLoaderAndSaver
    {
        private readonly IExchangeRatesRepository _ratesRepository;
        private readonly IApiCall _apiCall;

        public DataLoaderAndSaver(IApiCall apiCall, IExchangeRatesRepository ratesRepository)
        {
            _apiCall = apiCall;
            _ratesRepository = ratesRepository;
        }

        public Dictionary<string, decimal> LoadCurrencies(bool forceApi = false)
        {
            if (TableDontExistOrOutdated() || forceApi)
            {
                var apiData = LoadDataFromApi();
                SaveToDatabase(apiData);
                return apiData;
            }

            return LoadFromDb();
        }

        private Dictionary<string, decimal> LoadFromDb()
        {
            var ratesListDb = _ratesRepository.Load();
            var result = new Dictionary<string, decimal>();
            foreach (var currency in ratesListDb)
            {
                result.Add(currency.Name, currency.ToEur);
            }

            return result;
        }

        public void SimulateTimeChange()
        {
            LoadCurrencies(true);
        }

        public bool TableDontExistOrOutdated()
        {
           return _ratesRepository.InitializeAndCheckEmptyOrOutdated();
        }

        private Dictionary<string, decimal> LoadDataFromApi()
        {
            var currencyRates = _apiCall.GetRates();
            var baseCurrency = currencyRates.Base;
            var rates = currencyRates.Rates;
            rates.Add(baseCurrency, decimal.One);
            return rates;
        }

        private void SaveToDatabase(Dictionary<string, decimal> apiData)
        {
            _ratesRepository.SaveAndOverwrite(apiData, DateTime.Today);
        }
    }
}
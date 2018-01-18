using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.BusinessLogic.Interfaces;
using CurrencyConverter.DataAccess.Models;
using CurrencyConverter.DataAccess.Repositories;
using CurrencyConverter.Models;
using CurrencyConverter.ViewModels;

namespace CurrencyConverter.BusinessLogic
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

        public Dictionary<string, decimal> LoadCurrencies(int days)
        {
            if (_ratesRepository.IsOutDated(days))
            {
                var apiData = LoadDataFromApi();
                SaveToDatabase(apiData);
                return apiData;
            }
            else
            {
                var ratesListDb = _ratesRepository.Load();
                var result = new Dictionary<string, decimal>();
                foreach (var currency in ratesListDb)
                {
                    result.Add(currency.Name, currency.ToEur);
                }

                return result;
            }
        }

        public void SimulateTimeChange()
        {
            LoadCurrencies(-5);
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
            _ratesRepository.Save(apiData, DateTime.Today);
        }
    }
}
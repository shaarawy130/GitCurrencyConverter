using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autofac;
using Caliburn.Micro;
using CurrencyConverter.BusinessLogic;
using CurrencyConverter.BusinessLogic.Interfaces;
using CurrencyConverter.DataAccess;
using CurrencyConverter.DataAccess.Models;
using CurrencyConverter.DataAccess.Repositories;
using CurrencyConverter.Models;

namespace CurrencyConverter.ViewModels
{
    public class ConverterViewModel : INotifyPropertyChanged
    {
        private readonly ICurrencyConverterCalculations _currencyConverterCalculations;
        private readonly IDataLoaderAndSaver _dataLoaderAndSaver;
        private List<RateViewModel> _currenciesList;
        private decimal _fromCurrencyValue;
        private decimal _toCurrencyValue;
        private decimal _amount;
        private decimal _conversionResult;
        private RateViewModel _defaultFromCurrency;
        private RateViewModel _defaultToCurrency;
        private Dictionary<string, decimal> _rates;
        private string _show;
        private string _loading;

        public ConverterViewModel(ICurrencyConverterCalculations currencyConverterCalculations, IDataLoaderAndSaver dataLoaderAndSaver)
        {
            _show = "Collapsed";
            _loading = "Visible";
            _dataLoaderAndSaver = dataLoaderAndSaver;
            _currencyConverterCalculations = currencyConverterCalculations;
            PopulateRates();
        }
 
        public event PropertyChangedEventHandler PropertyChanged;
        ////public ICommand SubmitConversion => new DelegationCommand(SubmitConvert);
        ////public ICommand Toggle => new DelegationCommand(ToggleCurrencies);
        ////public ICommand Outdate => new DelegationCommand(OutdatedCall);

        public string Loading
        {
            get => _loading;
            set
            {
                _loading = value;
                OnPropertyChanged(nameof(Loading));
            }
        }
        public string Show
        {
            get => _show;
            set
            {
                Loading = value == "Collapsed" ? "Visible" : "Collapsed";
                _show = value;
                OnPropertyChanged(nameof(Show));
            }
        }
        public decimal ConversionResult
        {
            get => _conversionResult;
            set
            {
                _conversionResult = value;
                OnPropertyChanged(nameof(ConversionResult));
            }
        }
        public Dictionary<string, decimal> Rates
        {
            get => _rates;
            set
            {
                _rates = value;
                OnPropertyChanged(nameof(Rates));
            }
        }

        public List<RateViewModel> CurrenciesList
        {
            get => _currenciesList;

            set
            {
                _currenciesList = value;
                OnPropertyChanged(nameof(CurrenciesList));
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public decimal FromCurrencyValue
        {
            get => _fromCurrencyValue;

            set
            {
                _fromCurrencyValue = value;
                OnPropertyChanged(nameof(FromCurrencyValue));
            }
        }

        public RateViewModel DefaultToCurrency
        {
            get => _defaultToCurrency;
            set
            {
                _defaultToCurrency = value;
                OnPropertyChanged(nameof(DefaultToCurrency));
            }
        }

        public decimal ToCurrencyValue
        {
            get => _toCurrencyValue;

            set
            {
                _toCurrencyValue = value;
                OnPropertyChanged(nameof(ToCurrencyValue));
            }
        }

        public RateViewModel DefaultFromCurrency
        {
            get => _defaultFromCurrency;

            set
            {
                _defaultFromCurrency = value;
                OnPropertyChanged(nameof(DefaultFromCurrency));
            }
        }

        public void SubmitConvert()
        {
            ConversionResult = FromCurrencyValue < 0 || ToCurrencyValue < 0
                ? 0
                : _currencyConverterCalculations.Convert(FromCurrencyValue, ToCurrencyValue, Amount);
        }

        public void ToggleCurrencies()
        {
            var temp = DefaultFromCurrency;
            DefaultFromCurrency = DefaultToCurrency;
            DefaultToCurrency = temp;
            SubmitConvert();
        }

        public void OutdatedCall()
        {
            _dataLoaderAndSaver.SimulateTimeChange();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void PopulateRates()
        {
            Rates = await Task.Run(() => _dataLoaderAndSaver.LoadCurrencies(0));
            CurrenciesList = Rates.Select(x => new RateViewModel(x.Key, x.Value)).ToList();
            DefaultFromCurrency = CurrenciesList[CurrenciesList.Count - 1];
            DefaultToCurrency = CurrenciesList[0];
            Show = "Visible";
        }
    }
}

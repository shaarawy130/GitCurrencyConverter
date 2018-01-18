using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using ConverterPresentation.Annotations;
using ConverterPresentation.Messages;
using ConverterPresentation.Models;

namespace ConverterPresentation.ViewModels
{
    public class SelectionRateViewModel : INotifyPropertyChanged
    {
        private Rate _defaultToCurrency;
        private Rate _defaultFromCurrency;
        private readonly IEventAggregator _eventAggregator;
        private decimal _fromCurrencyValue;
        private decimal _toCurrencyValue;

        public SelectionRateViewModel() { }

        public SelectionRateViewModel(Dictionary<string, decimal> curencyRatesDic, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CurrenciesRateList = curencyRatesDic.Select(x => new Rate(x.Key, x.Value)).ToList();
            DefaultFromCurrency = CurrenciesRateList[CurrenciesRateList.Count - 1];
            DefaultToCurrency = CurrenciesRateList[0];
        }

        public Rate DefaultToCurrency
        {
            get => _defaultToCurrency;

            set
            {
                _defaultToCurrency = value;
                ToCurrencyValue = _defaultToCurrency.RateValue;
            }
        }

        public decimal FromCurrencyValue
        {
            get => _fromCurrencyValue;

            set
            {
                _fromCurrencyValue = value;
                FromSelectedCurrencyChangedEvent();
            }
        }
        public decimal ToCurrencyValue
        {
            get => _toCurrencyValue;

            set
            {
                _toCurrencyValue = value;
                ToSelectedCurrencyChangedEvent();
            }
        }
        public Rate DefaultFromCurrency
        {
            get => _defaultFromCurrency;

            set
            {
                _defaultFromCurrency = value;
                FromCurrencyValue = _defaultFromCurrency.RateValue;
            }
        }

        public List<Rate> CurrenciesRateList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public void FromSelectedCurrencyChangedEvent()
        {
               
            _eventAggregator.PublishOnUIThread(new FromCurrencyChangedMessage(FromCurrencyValue));
        }
        public void ToSelectedCurrencyChangedEvent()
        {
            _eventAggregator.PublishOnUIThread(new ToCurrencyChangedMessage(ToCurrencyValue));
        }

        public void ToggleCurrencies()
        {
            var temp = DefaultFromCurrency;
            DefaultFromCurrency = DefaultToCurrency;
            DefaultToCurrency = temp;
            _eventAggregator.PublishOnUIThread(new ToggledMessage());
        }

    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using ConverterBusinessLogic.Interfaces;
using ConverterPresentation.Messages;

namespace ConverterPresentation.ViewModels
{
    public class ConverterViewModel : INotifyPropertyChanged, IHandle<FromCurrencyChangedMessage>, IHandle<ToCurrencyChangedMessage>, IHandle<ToggledMessage>
    {
        private readonly ICurrencyConverterCalculations _currencyConverterCalculations;
        private readonly IDataLoaderAndSaver _dataLoaderAndSaver;
        private readonly IEventAggregator _eventAggregator;
        private string _show;


        public ConverterViewModel(
            ICurrencyConverterCalculations currencyConverterCalculations,
            IDataLoaderAndSaver dataLoaderAndSaver,
            IEventAggregator eventAggregator)
        {
            _show = "Collapsed";
            Loading = "Visible";
            _dataLoaderAndSaver = dataLoaderAndSaver;
            _eventAggregator = eventAggregator;
            _currencyConverterCalculations = currencyConverterCalculations;

            _eventAggregator.Subscribe(this);

            PopulateRates();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        ////public ICommand SubmitConversion => new DelegationCommand(SubmitConvert);
        ////public ICommand Toggle => new DelegationCommand(ToggleCurrencies);
        ////public ICommand Outdate => new DelegationCommand(OutdatedCall);


        public SelectionRateViewModel ComboBoxSelectionRateViewModel { get; set; } = new SelectionRateViewModel();

        public string Loading { get; set; }

        public string Show
        {
            get => _show;
            set
            {
                Loading = value == "Collapsed" ? "Visible" : "Collapsed";
                _show = value;
            }
        }

        public decimal ConversionResult { get; set; }

        public Dictionary<string, decimal> Rates { get; set; }


        public decimal Amount { get; set; }

        public decimal FromCurrencyValue { get; set; }

        public decimal ToCurrencyValue { get; set; }


        public void SubmitConvert()
        {
            ConversionResult = FromCurrencyValue < 0 || ToCurrencyValue < 0
                ? 0
                : _currencyConverterCalculations.Convert(FromCurrencyValue, ToCurrencyValue, Amount);
        }



        public void OutdatedCall()
        {
            _dataLoaderAndSaver.SimulateTimeChange();
        }



        private async void PopulateRates()
        {
            Rates = await Task.Run(() => _dataLoaderAndSaver.LoadCurrencies());
            ComboBoxSelectionRateViewModel = new SelectionRateViewModel(Rates, _eventAggregator);
            Show = "Visible";
        }

        public void Handle(FromCurrencyChangedMessage message)
        {
            FromCurrencyValue = message.FromCurrencyValue;
        }

        public void Handle(ToCurrencyChangedMessage message)
        {
            ToCurrencyValue = message.ToCurrencyValue;
        }

        public void Handle(ToggledMessage message)
        {
            SubmitConvert();
        }
    }

}

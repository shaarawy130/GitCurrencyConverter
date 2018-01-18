using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Caliburn.Micro;
using CurrencyConverter.BusinessLogic;
using CurrencyConverter.BusinessLogic.Interfaces;
using CurrencyConverter.DataAccess;
using CurrencyConverter.DataAccess.Mapping;
using CurrencyConverter.DataAccess.Models;
using CurrencyConverter.DataAccess.Repositories;
using CurrencyConverter.Models;
using CurrencyConverter.ViewModels;
using IContainer = Autofac.IContainer;

namespace CurrencyConverter
{
    class IoCBuilder
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CalculateConversionCalculations>().As<ICurrencyConverterCalculations>().SingleInstance();
            builder.RegisterType<ExchangeRatesRepository>().As<IExchangeRatesRepository>();
            builder.RegisterType<WindowManager>().As<IWindowManager>();
            builder.RegisterType<DataLoaderAndSaver>().As<IDataLoaderAndSaver>();
            builder.RegisterType<ExchangeRates>();
            builder.RegisterType<CurrencyRates>();
            builder.RegisterType<ExchangeRateMap>();
            builder.RegisterType<ApiCall>().As<IApiCall>();
            builder.RegisterType<RateViewModel>();
            builder.RegisterType<ConverterViewModel>();
            return builder.Build();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ConverterDataAccess.Mapping;
using ConverterDataAccess.Models;
using ConverterDataAccess.Repositories;
using ConverterDataAccess.Repositories.Interfaces;

namespace ConverterDataAccess
{
    public class IoCBuilder
    {
        public static void Build(ContainerBuilder builder)
        {
            builder.RegisterType<ExchangeRates>();
            builder.RegisterType<ExchangeRatesRepository>().As<IExchangeRatesRepository>();
            builder.RegisterType<CreateSession>().As<ICreateSession>().SingleInstance();
            builder.RegisterType<ExchangeRateMap>();
            
        }
    }
}

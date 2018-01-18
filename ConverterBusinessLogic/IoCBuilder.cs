using Autofac;
using ConverterBusinessLogic.Interfaces;
using ConverterBusinessLogic.Model;
using ConverterBusinessLogic.Service;

namespace ConverterBusinessLogic
{
   public class IoCBuilder
    {
        public static void Build(ContainerBuilder builder)
        {
            builder.RegisterType<CalculateConversionCalculations>().As<ICurrencyConverterCalculations>();
            builder.RegisterType<DataLoaderAndSaver>().As<IDataLoaderAndSaver>();
            builder.RegisterType<ApiCall>().As<IApiCall>();
            builder.RegisterType<CurrencyRates>();

            ConverterDataAccess.IoCBuilder.Build(builder);
        }
    }
}

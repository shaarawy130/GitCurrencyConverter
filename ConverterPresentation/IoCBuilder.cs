using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Caliburn.Micro;
using ConverterPresentation.Messages;
using ConverterPresentation.Models;
using ConverterPresentation.ViewModels;
using IContainer = Autofac.IContainer;

namespace ConverterPresentation
{
    public class IoCBuilder
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WindowManager>().As<IWindowManager>();
            builder.RegisterType<SelectionRateViewModel>();
            builder.RegisterType<ConverterViewModel>();
            builder.RegisterType<Rate>();
            builder.RegisterType<FromCurrencyChangedMessage>();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<ToCurrencyChangedMessage>();
            ConverterBusinessLogic.IoCBuilder.Build(builder);
            return builder.Build();
        }
    }
}

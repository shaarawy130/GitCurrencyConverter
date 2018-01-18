using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using CurrencyConverter.ViewModels;

namespace CurrencyConverter
{
    class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }
        private static IContainer Container { get; set; }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
             Container = IoCBuilder.Build();
            DisplayRootViewFor<ConverterViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return Container.Resolve(service);
        }
    }
}

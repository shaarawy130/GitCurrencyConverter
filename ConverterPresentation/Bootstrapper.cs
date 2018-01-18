using System;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using ConverterPresentation.ViewModels;
using IContainer = Autofac.IContainer;

namespace ConverterPresentation
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.BusinessLogic;
using CurrencyConverter.DataAccess.Models;
using CurrencyConverter.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace CurrencyConverter.DataAccess.Repositories
{
    public class ExchangeRatesRepository : IExchangeRatesRepository
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    CreateFactorySession(false);

                return _sessionFactory;
            }
        }
      
        // handel loading , saving
        public void Save(Dictionary<string, decimal> currenciesFromApi, DateTime date)
        {
            CreateFactorySession(true);
            var ratesDic =  currenciesFromApi;
            foreach (var curr in ratesDic)
            {
                using (ISession session = _sessionFactory.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(new ExchangeRates()
                    {
                        Name = curr.Key,
                        ToEur = curr.Value,
                        DateExtracted = date
                    });
                    transaction.Commit();
                }
            }
        }

        public List<ExchangeRates> Load()
        {
            CreateFactorySession(false);
            using (ISession session = _sessionFactory.OpenSession())
            {
                    var exchangeRatesList = session.Query<ExchangeRates>().ToList();
               
                return exchangeRatesList;
            }
        }

        public bool IsOutDated(int days)
        {
            CreateFactorySession(false);
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.Query<ExchangeRates>().Any(x => x.DateExtracted <= DateTime.Today.AddDays(days));
            }
        }

        private static void CreateFactorySession(bool state)
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString("Server=(localdb)\\mssqllocaldb;Database=CurrencyConverter;Trusted_Connection=True;MultipleActiveResultSets=true"))
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<ExchangeRates>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(state, state))
                .BuildSessionFactory();
        }
    }
}

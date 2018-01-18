using System;
using System.Collections.Generic;
using System.Linq;
using ConverterDataAccess.Models;
using ConverterDataAccess.Repositories.Interfaces;
using NHibernate;

namespace ConverterDataAccess.Repositories
{
    public class ExchangeRatesRepository : IExchangeRatesRepository
    {
        private static ISessionFactory _sessionFactory;
        private readonly ICreateSession _createSession;

        public ExchangeRatesRepository( ICreateSession createSession)
        {
            _createSession = createSession;
            _sessionFactory = _createSession.CreateFactorySessionAndUpdateTable();
        }


        public void SaveAndOverwrite(Dictionary<string, decimal> currenciesFromApi, DateTime date)
        {
            var exchangeRateList = Load();
            var ratesDic = currenciesFromApi;

            

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var curr in ratesDic)
                {
                    var selected = exchangeRateList.FirstOrDefault(x => x.Name == curr.Key);
                    selected = CreateOrUpdateExchangeRates(date, selected, curr);
                    session.SaveOrUpdate(selected);
                }
                transaction.Commit();
            }
        }

        private static ExchangeRates CreateOrUpdateExchangeRates(DateTime date, ExchangeRates selected, KeyValuePair<string, decimal> curr)
        {
            if (selected != null)
            {
                selected.ToEur = curr.Value;
                selected.DateExtracted = date;
            }
            else
            {
                selected = new ExchangeRates()
                    {Name = curr.Key, ToEur = curr.Value, DateExtracted = date};
            }

            return selected;
        }


        public List<ExchangeRates> Load()
        {

            using (ISession session = _sessionFactory.OpenSession())
            {
                var exchangeRatesList = session.Query<ExchangeRates>().ToList();
                return exchangeRatesList;
            }
        }

        public bool InitializeAndCheckEmptyOrOutdated()
        {
            var sessionFactory = _createSession.CreateFactorySessionAndUpdateTable(true);
            using (ISession session = sessionFactory.OpenSession())
            {
                if (!session.Query<ExchangeRates>().Any())
                    return true;
                return session.Query<ExchangeRates>().Any(x => x.DateExtracted < DateTime.Today.AddDays(-2));
            }
        }
    }
    
}
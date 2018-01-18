using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConverterDataAccess.Models;
using ConverterDataAccess.Repositories.Interfaces;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace ConverterDataAccess.Repositories
{
    public class CreateSession :ICreateSession
    {
        public ISessionFactory CreateFactorySessionAndUpdateTable(bool state = false)
        {
           return  Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString("Server=(localdb)\\mssqllocaldb;Database=CurrencyConverter;Trusted_Connection=True;MultipleActiveResultSets=true"))
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<ExchangeRates>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(state, state))
                .BuildSessionFactory();
        }
    }
}

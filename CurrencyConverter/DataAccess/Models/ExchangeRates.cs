using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.DataAccess.Models
{
    public class ExchangeRates
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateExtracted { get; set; }
        public virtual decimal ToEur { get; set; }
    }
}

// take data from database , get data from internet if newer

// 1. generate model
// 2. add nuget packets (nhibernate & fluent , automapper)
// 3. configure and use nhibernate (with localdb inside visual studio)

// carefull: only use dataAccess on businesslogic!
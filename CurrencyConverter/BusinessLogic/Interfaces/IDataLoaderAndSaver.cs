using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.BusinessLogic.Interfaces
{
    public interface IDataLoaderAndSaver
    {
        Dictionary<string, decimal> LoadCurrencies(int days);
        void SimulateTimeChange();
    }
}

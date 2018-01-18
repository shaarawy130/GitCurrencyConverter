using System.Collections.Generic;

namespace ConverterBusinessLogic.Interfaces
{
    public interface IDataLoaderAndSaver
    {
        Dictionary<string, decimal> LoadCurrencies( bool forceApiLoad = false);
        void SimulateTimeChange();
        bool TableDontExistOrOutdated();
    }
}
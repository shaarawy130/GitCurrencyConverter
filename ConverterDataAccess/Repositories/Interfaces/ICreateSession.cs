using NHibernate;

namespace ConverterDataAccess.Repositories.Interfaces
{
   public interface ICreateSession
    {
        ISessionFactory CreateFactorySessionAndUpdateTable( bool state = false);
    }
}

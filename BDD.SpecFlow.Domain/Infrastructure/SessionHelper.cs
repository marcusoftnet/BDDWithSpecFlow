using NHibernate;

namespace BDD.SpecFlow.Domain.Infrastructure
{
    public interface SessionHelper
    {
        ISession GetCurrentSession();
    }
}
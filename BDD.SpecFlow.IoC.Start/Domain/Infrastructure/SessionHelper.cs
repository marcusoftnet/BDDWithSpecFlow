using NHibernate;
using StructureMap;

namespace BDD.SpecFlow.IoC.Start.Domain.Infrastructure
{
    /// <summary>
    /// eftersom vi kör en kommandoradsapplikation
    /// kan vi ha Session-helper som en singleton
    /// </summary>
    [PluginFamily(IsSingleton = true)]
    public interface SessionHelper
    {
        ISession GetCurrentSession();
        void StartSession();
        void CloseSession();
    }
}
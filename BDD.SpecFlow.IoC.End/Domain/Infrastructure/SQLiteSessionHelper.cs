using FluentNHibernate;
using NHibernate;

namespace BDD.SpecFlow.IoC.End.Domain.Infrastructure
{
    public class SQLiteSessionHelper : SessionHelper
    {
        private readonly ISessionSource _sessionSource;
        private ISession _session;

        public SQLiteSessionHelper(ISessionSource sessionSource)
        {
            _sessionSource = sessionSource;
        }

        public ISession GetCurrentSession()
        {
            return _session;
        }

        public void StartSession()
        {
            _session = _sessionSource.CreateSession();
        }

        public void CloseSession()
        {
            _session.Close();
            _session.Dispose();
        }
    }
}
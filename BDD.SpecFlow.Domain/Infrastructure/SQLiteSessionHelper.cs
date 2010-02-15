using System;
using FluentNHibernate;
using NHibernate;

namespace BDD.SpecFlow.Domain.Infrastructure
{
    public class SQLiteSessionHelper : SessionHelper, IDisposable
    {
        private ISession _session;

        public SQLiteSessionHelper(ISessionSource sessionSource)
        {
            _session = sessionSource.CreateSession();
        }
        public ISession GetCurrentSession()
        {
            return _session;
        }

        public void Dispose()
        {
            _session.Close();
        }
    }
}
using BDD.SpecFlow.Domain.Infrastructure;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NUnit.Framework;

namespace BDD.SpecFlow.Specs.Infrastructure
{

    public abstract class PersistanceFixtureBase
    {
        protected SessionSource _sessionSource { get; set; }
        protected ISession _session { get; private set; }

        [SetUp]
        public void SetupContext()
        {
            var cfg = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ShowSql().InMemory);

            var model = FluentNHibernateHelper.GetAutomappedPeristanceModel();

            _sessionSource = new SessionSource(cfg.BuildConfiguration().Properties, model);
            _session = _sessionSource.CreateSession();
            _sessionSource.BuildSchema(_session);
        }

        [TearDown]
        public void TearDownContext()
        {
            _session.Close();
            _session.Dispose();
        }
    }
}



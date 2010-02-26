using BDD.SpecFlow.IoC.Start.Domain.Infrastructure;
using BDD.SpecFlow.IoC.Start.Domain.Model;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NUnit.Framework;

namespace BDD.SpecFlow.IoC.Start.Design
{
    [TestFixture]
    public class FilmRepositoryExempel
    {
        private SessionSource _sessionSource;
        private SQLiteSessionHelper _sessionHelper;

        [SetUp]
        public void Setup()
        {
            var model = NHibernateHelper.SkapaPeristanceModel();

            var cfg = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ShowSql().InMemory);

            _sessionSource = new SessionSource(cfg.BuildConfiguration().Properties, model);

            _sessionHelper = new SQLiteSessionHelper(_sessionSource);
            _sessionHelper.StartSession();
            _sessionSource.BuildSchema(_sessionHelper.GetCurrentSession());
        }

        [TearDown]
        public void TearDown()
        {
            _sessionHelper.CloseSession();
        }

        [Test]
        public void ska_hämta_en_tom_lista_då_inga_filmer_finns_i_databasen()
        {
            // Arrange
            var rep = new SQLiteFilmRepository(_sessionHelper);

            // act
            var allaFilmer = rep.HämtaAlla();

            // Arrange
            Assert.That(allaFilmer, Is.Empty);
        }
    }
}



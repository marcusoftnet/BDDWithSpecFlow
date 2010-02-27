using BDD.SpecFlow.Mock.Start.Domain.Model.Entitet;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;

namespace BDD.SpecFlow.Mock.Start.Domain.Infrastructure
{
    public class NHibernateHelper
    {
        /// <summary>
        /// Returns the automapped persistance model with conventions
        /// </summary>
        /// <returns></returns>
        public static PersistenceModel SkapaPeristanceModel()
        {
            // Vi köper alla default konventioner
            return AutoMap.AssemblyOf<Film>()
                .Where(type => type.IsClass &&
                               !type.IsAbstract &&
                               type.Namespace.EndsWith("Entitet"));
        }

        public static ISessionSource KonfigureraDatabas(string databasFil)
        {
            var cfg = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(databasFil));

            var model = NHibernateHelper.SkapaPeristanceModel();
            return new SessionSource(cfg.BuildConfiguration().Properties, model);
        }
    }
}



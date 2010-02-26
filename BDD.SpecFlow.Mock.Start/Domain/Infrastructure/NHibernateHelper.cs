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

    ///// <summary>
    ///// Setting the name of Many-to-Many tables
    ///// </summary>
    //public class CustomManyToManyTableNameConvention : ManyToManyTableNameConvention
    //{
    //    protected override string GetBiDirectionalTableName(IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide)
    //    {
    //        return Inflector.Net.Inflector.Pluralize(collection.EntityType.Name) + "_" + Inflector.Net.Inflector.Pluralize(otherSide.EntityType.Name);
    //    }

    //    protected override string GetUniDirectionalTableName(IManyToManyCollectionInspector collection)
    //    {
    //        return Inflector.Net.Inflector.Pluralize(collection.EntityType.Name) + "_" + Inflector.Net.Inflector.Pluralize(collection.ChildType.Name);
    //    }
    //}

    ///// <summary>
    ///// I use this convention to get cascade all on all the entities 
    ///// that implements IAggregateRoot (empty interface), so that I 
    ///// can set up a casacade-strategy
    ///// </summary>
    ///// <seealso cref="http://wiki.fluentnhibernate.org/Available_conventions#Interfaces"/>
    //public class CustomHasManyConvention : IHasManyConvention, IHasManyConventionAcceptance
    //{
    //    public void Accept(IAcceptanceCriteria<IOneToManyCollectionInspector> criteria)
    //    {
    //        // Hmmm - not to happy about the GetInterface(typeof(IAggregateRoot).Name)
    //        // but it will have to work for now
    //        criteria.Expect(x => x.EntityType.GetInterface(typeof(IAggregateRoot).Name) != null);
    //    }

    //    public void Apply(IOneToManyCollectionInstance instance)
    //    {
    //        instance.Cascade.All();
    //    }
    //}

    ///// <summary>
    ///// I use this convention to get cascade all on all the entities 
    ///// that implements IAggregateRoot (empty interface), so that I 
    ///// can set up a casacade-strategy
    ///// </summary>
    ///// <seealso cref="http://wiki.fluentnhibernate.org/Available_conventions#Interfaces"/>
    //public class CustomHasManyToManyConvention : IHasManyToManyConvention, IHasManyToManyConventionAcceptance
    //{
    //    public void Apply(IManyToManyCollectionInstance instance)
    //    {
    //        instance.Cascade.All();
    //    }

    //    public void Accept(IAcceptanceCriteria<IManyToManyCollectionInspector> criteria)
    //    {
    //        criteria.Expect(x => x.EntityType.GetInterface(typeof(IAggregateRoot).Name) != null);
    //    }
    //}
}



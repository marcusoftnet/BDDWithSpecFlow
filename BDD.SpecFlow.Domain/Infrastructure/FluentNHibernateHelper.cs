using BDD.SpecFlow.Domain.Model;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Automapping;

namespace BDD.SpecFlow.Domain.Infrastructure
{
    public class FluentNHibernateHelper
    {
        

        /// <summary>
        /// Returns the automapped persistance model with conventions
        /// </summary>
        /// <returns></returns>
        public static AutoPersistenceModel GetAutomappedPeristanceModel()
        {
            return AutoMap.AssemblyOf<Film>()
                .Where(t => t.Namespace.EndsWith("Model") && t.IsAbstract == false)
                .Conventions.Add(
                                    PrimaryKey.Name.Is(pk => "ID"),
                                    ForeignKey.EndsWith("ID"),
                                    Table.Is(t => Inflector.Net.Inflector.Pluralize(t.EntityType.Name)))
                .Conventions.Add<CustomManyToManyTableNameConvention>()
                .Conventions.Add<CustomHasManyConvention>()
                .Conventions.Add<CustomHasManyToManyConvention>();
        }
    }

    /// <summary>
    /// Setting the name of Many-to-Many tables
    /// </summary>
    public class CustomManyToManyTableNameConvention : ManyToManyTableNameConvention
    {
        protected override string GetBiDirectionalTableName(IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide)
        {
            return Inflector.Net.Inflector.Pluralize(collection.EntityType.Name) + "_" + Inflector.Net.Inflector.Pluralize(otherSide.EntityType.Name);
        }

        protected override string GetUniDirectionalTableName(IManyToManyCollectionInspector collection)
        {
            return Inflector.Net.Inflector.Pluralize(collection.EntityType.Name) + "_" + Inflector.Net.Inflector.Pluralize(collection.ChildType.Name);
        }
    }

    /// <summary>
    /// I use this convention to get cascade all on all the entities 
    /// that implements IAggregateRoot (empty interface), so that I 
    /// can set up a casacade-strategy
    /// </summary>
    /// <seealso cref="http://wiki.fluentnhibernate.org/Available_conventions#Interfaces"/>
    public class CustomHasManyConvention : IHasManyConvention, IHasManyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IOneToManyCollectionInspector> criteria)
        {
            // Hmmm - not to happy about the GetInterface(typeof(IAggregateRoot).Name)
            // but it will have to work for now
            criteria.Expect(x => x.EntityType.GetInterface(typeof(IAggregateRoot).Name) != null);
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }
    }

    /// <summary>
    /// I use this convention to get cascade all on all the entities 
    /// that implements IAggregateRoot (empty interface), so that I 
    /// can set up a casacade-strategy
    /// </summary>
    /// <seealso cref="http://wiki.fluentnhibernate.org/Available_conventions#Interfaces"/>
    public class CustomHasManyToManyConvention : IHasManyToManyConvention, IHasManyToManyConventionAcceptance
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }

        public void Accept(IAcceptanceCriteria<IManyToManyCollectionInspector> criteria)
        {
            criteria.Expect(x => x.EntityType.GetInterface(typeof(IAggregateRoot).Name) != null);
        }
    }
}



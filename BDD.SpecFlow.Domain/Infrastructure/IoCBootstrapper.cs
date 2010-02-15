using StructureMap;
using StructureMap.Configuration.DSL;

namespace BDD.SpecFlow.Domain.Infrastructure
{
    public static class IoCBootstrapper
    {
        public static void Init()
        {
            StructureMapConfiguration.AddRegistry(new MovieRegistry());
        }
    }

    public class MovieRegistry : Registry
    {
        protected override void configure()
        {
            //ForRequestedType<IRepository>()
            //    .TheDefaultIsConcreteType<MyRepository>();
        }
    }
}



using System.Collections.Generic;
using BDD.SpecFlow.Mock.Start.Domain.Model.Entitet;
using StructureMap;

namespace BDD.SpecFlow.Mock.Start.Domain.Model
{
    [PluginFamily(IsSingleton = true)]    
    public interface FilmRepository
    {
        IList<Film> HämtaAlla();
    }
}
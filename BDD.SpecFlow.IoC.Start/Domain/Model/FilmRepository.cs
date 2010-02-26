using System.Collections.Generic;
using BDD.SpecFlow.IoC.Start.Domain.Model.Entitet;
using StructureMap;

namespace BDD.SpecFlow.IoC.Start.Domain.Model
{
    [PluginFamily(IsSingleton = true)]    
    public interface FilmRepository
    {
        IList<Film> HämtaAlla();
    }
}
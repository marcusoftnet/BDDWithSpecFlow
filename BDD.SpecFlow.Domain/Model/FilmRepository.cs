using System.Collections.Generic;
using BDD.SpecFlow.Domain.Model.Entitet;
using StructureMap;

namespace BDD.SpecFlow.Domain.Model
{
    [PluginFamily(IsSingleton = true)]    
    public interface FilmRepository
    {
        IList<Film> HämtaAlla();
    }
}
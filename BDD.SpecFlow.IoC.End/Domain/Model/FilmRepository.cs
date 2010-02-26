using System.Collections.Generic;
using BDD.SpecFlow.IoC.End.Domain.Model.Entitet;
using StructureMap;

namespace BDD.SpecFlow.IoC.End.Domain.Model
{
    [PluginFamily(IsSingleton = true)]    
    public interface FilmRepository
    {
        IList<Film> HämtaAlla();
    }
}
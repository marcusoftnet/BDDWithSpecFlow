using System.Collections.Generic;
using BDD.SpecFlow.Mock.End.Domain.Model.Entitet;
using StructureMap;

namespace BDD.SpecFlow.Mock.End.Domain.Model
{
    [PluginFamily(IsSingleton = true)]    
    public interface FilmRepository
    {
        IList<Film> HämtaAlla();
    }
}
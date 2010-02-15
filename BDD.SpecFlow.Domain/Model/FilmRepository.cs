using System.Collections.Generic;
using BDD.SpecFlow.Domain.Model.Entitet;

namespace BDD.SpecFlow.Domain.Model
{
    public interface FilmRepository
    {
        IList<Film> HämtaAlla();
    }
}
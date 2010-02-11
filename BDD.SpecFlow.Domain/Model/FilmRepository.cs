using System.Collections.Generic;

namespace BDD.SpecFlow.Domain.Model
{
    public interface FilmRepository
    {
        IList<Film> HämtaAlla();
    }
}
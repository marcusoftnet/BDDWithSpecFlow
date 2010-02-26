using System.Collections.Generic;
using BDD.SpecFlow.IoC.Start.Domain.Infrastructure;
using BDD.SpecFlow.IoC.Start.Domain.Model.Entitet;

namespace BDD.SpecFlow.IoC.Start.Domain.Model
{
    public class SQLiteFilmRepository : FilmRepository
    {
        private readonly SessionHelper _sessionHelper;

        public SQLiteFilmRepository(SessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
        }

        public IList<Film> HämtaAlla()
        {
            var session = _sessionHelper.GetCurrentSession();

            return session.CreateCriteria<Film>().List<Film>();
        }
    }
}
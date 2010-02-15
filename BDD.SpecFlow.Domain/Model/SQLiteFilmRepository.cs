using System.Collections.Generic;
using BDD.SpecFlow.Domain.Infrastructure;
using BDD.SpecFlow.Domain.Model.Entitet;

namespace BDD.SpecFlow.Domain.Model
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

            return session.CreateCriteria(typeof(Film)).List<Film>();
        }
    }
}
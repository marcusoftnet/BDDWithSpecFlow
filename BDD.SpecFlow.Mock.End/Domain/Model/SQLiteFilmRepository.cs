﻿using System.Collections.Generic;
using BDD.SpecFlow.Mock.End.Domain.Infrastructure;
using BDD.SpecFlow.Mock.End.Domain.Model.Entitet;

namespace BDD.SpecFlow.Mock.End.Domain.Model
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
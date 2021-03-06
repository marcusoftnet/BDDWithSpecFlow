﻿using BDD.SpecFlow.IoC.End.Domain.Model;
using BDD.SpecFlow.IoC.End.Domain.Vyer;
using FluentNHibernate;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace BDD.SpecFlow.IoC.End.Domain.Infrastructure
{
    public static class IoCBootstrapper
    {
        public static void Init()
        {
            ObjectFactory.Initialize(x => x.AddRegistry(new MovieRegistry()));
        }
    }

    public class MovieRegistry : Registry
    {
        protected override void configure()
        {
            ForRequestedType<ISessionSource>()
                .TheDefault.Is.ConstructedBy(x => NHibernateHelper.KonfigureraDatabas("Filmsamling_Test.db"));

            ForRequestedType<SessionHelper>()
                .TheDefault.Is.OfConcreteType<SQLiteSessionHelper>();

            ForRequestedType<FilmRepository>()
                .TheDefault.Is.OfConcreteType<SQLiteFilmRepository>();

            ForRequestedType<VyRepository>()
                .TheDefault.Is.OfConcreteType<MinnesVyRepository>();

            ForRequestedType<Filmsamling>()
                .TheDefault.Is.OfConcreteType<Filmsamling>();
            ForRequestedType<KommandoKälla>()
                .TheDefault.Is.OfConcreteType<KonsolKommandoKälla>();
        }
    }
}



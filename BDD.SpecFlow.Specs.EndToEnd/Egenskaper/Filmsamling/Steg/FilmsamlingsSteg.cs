using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BDD.SpecFlow.Domain.Infrastructure;
using BDD.SpecFlow.Domain.Model;
using BDD.SpecFlow.Domain.Model.Entitet;
using BDD.SpecFlow.Domain.Vyer;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Moq;
using NUnit.Framework;
using StructureMap;
using TechTalk.SpecFlow;

namespace BDD.SpecFlow.Specs.EndToEnd.Egenskaper.Filmsamling.Steg
{
    [Binding]
    public class FilmsamlingsSteg
    {
        private ISessionSource _sessionSource;
        private Mock<TextWriter> _mockSystemOut;

        [Given(@"att min filmsamling är tom")]
        public void GivetAttMinFilmsamlingÄrTom()
        {
            // Initera Ioc
            IoCBootstrapper.Init();

            // Sätta upp tom databas med korrekt schema
            _sessionSource = ObjectFactory.GetInstance<ISessionSource>();
            _sessionSource.BuildSchema();
        }

        [When(@"jag anger kommando: (.+)$")]
        public void NärJagAngerKommando(string kommando)
        {
            // Skapad stubbad vy
            _mockSystemOut = new Mock<TextWriter>();
            var antalFilmerVy = new AntalFilmerVy(_mockSystemOut.Object);

            // Hämta vyrepository, som är en singleton, från IoC och lägg till stubb-vy
            var vyRepository = ObjectFactory.GetInstance<VyRepository>();
            vyRepository.LäggTill(VyNamn.ANTAL_FILMER, antalFilmerVy);
            
            // Skapa filmsamling
            var kommandoKälla = ObjectFactory.GetInstance<Domain.Model.KommandoKälla>();

            // Kör kommandot
            kommandoKälla.KörKommando(kommando);
        }
        [Then(@"ska resultatet vara: (.+)$")]
        public void SåSkaResultatetVara(string resultat)
        {
            // Verifiera mot stubimpl av vy
            _mockSystemOut.Verify(x => x.WriteLine(resultat), Times.Once());
        }
    }
}

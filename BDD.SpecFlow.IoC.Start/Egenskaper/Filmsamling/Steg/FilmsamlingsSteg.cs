using System.IO;
using BDD.SpecFlow.IoC.Start.Domain.Infrastructure;
using BDD.SpecFlow.IoC.Start.Domain.Model.Entitet;
using BDD.SpecFlow.IoC.Start.Domain.Vyer;
using FluentNHibernate;
using Moq;
using StructureMap;
using TechTalk.SpecFlow;

namespace BDD.SpecFlow.IoC.Start.Egenskaper.Filmsamling.Steg
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

        private static void Skapa5TestFilmerIDatabasen(ISessionSource sessionSource)
        {
            using (var session = sessionSource.CreateSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(new Film { Id = 1, Name = "Alien" });
                    session.Save(new Film { Id = 2, Name = "E.T." });
                    session.Save(new Film { Id = 3, Name = "Matrix[][]" });
                    session.Save(new Film { Id = 4, Name = "Broarna i Maddison County" });
                    session.Save(new Film { Id = 5, Name = "Jaws" });

                    session.Flush();

                    tx.Commit();
                }
            }
        }

    }
}



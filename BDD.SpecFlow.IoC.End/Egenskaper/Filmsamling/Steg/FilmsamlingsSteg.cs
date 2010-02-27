using System.Collections.Generic;
using System.IO;
using System.Linq;
using BDD.SpecFlow.IoC.End.Domain.Infrastructure;
using BDD.SpecFlow.IoC.End.Domain.Model.Entitet;
using BDD.SpecFlow.IoC.End.Domain.Vyer;
using FluentNHibernate;
using Moq;
using StructureMap;
using TechTalk.SpecFlow;

namespace BDD.SpecFlow.IoC.End.Egenskaper.Filmsamling.Steg
{
    [Binding]
    public class FilmsamlingsSteg
    {
        private ISessionSource _sessionSource;
        private Mock<TextWriter> _mockSystemOut;

        public FilmsamlingsSteg()
        {

            // Initera Ioc
            IoCBootstrapper.Init();
            
            // Sätta upp tom databas med korrekt schema
            _sessionSource = ObjectFactory.GetInstance<ISessionSource>();
            _sessionSource.BuildSchema();
            
            // Skapad stubbade vyer
            _mockSystemOut = new Mock<TextWriter>();
            var antalFilmerVy = new AntalFilmerVy(_mockSystemOut.Object);
            var listaFilmerVy = new ListaFilmerVy(_mockSystemOut.Object);

            // Hämta vyrepository, som är en singleton, från IoC och lägg till stubb-vyerna
            var vyRepository = ObjectFactory.GetInstance<VyRepository>();
            vyRepository.LäggTill(VyNamn.ANTAL_FILMER, antalFilmerVy);
            vyRepository.LäggTill(VyNamn.LISTA_FILMER, listaFilmerVy);

        }

        [Given(@"att min filmsamling är tom")]
        public void GivetAttMinFilmsamlingÄrTom()
        { }

        [Given(@"att filmsamlingen innehåller (.+)$")]
        public void GivetAttFilmsamlingenInnehallerFilmListan(string filmListaSträng)
        {
            // Skapa testdata filmer
            var filmer = IndataSträngTillFilmLista(filmListaSträng); 
            SkapaTestFilmerIDatabasen(_sessionSource, filmer);
        }


        [When(@"jag anger kommando: (.+)$")]
        public void NärJagAngerKommando(string kommando)
        {
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

        [Then(@"ska resultatet vara:")]
        public void SåSkaResultatetVara(Table utdataRader)
        {
            // TODO: Fix to verify each call - don't know the Moq-syntax for that ... yet
            _mockSystemOut.Verify(x => x.WriteLine(utdataRader.Rows[0]["Rad"]), Times.Once());

            //foreach (var row in utdataRader.Rows)
            //{
            //    var utdataRad = row["Rad"];
            //    _mockSystemOut.Verify(x => x.WriteLine(utdataRad), Times.Once());
            //}
        }


        private static void SkapaTestFilmerIDatabasen(ISessionSource sessionSource, IList<Film> filmer)
        {
            using (var session = sessionSource.CreateSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    foreach (var film in filmer)
                    {
                        session.Save(film);
                    }

                    session.Flush();
                    tx.Commit();
                }
            }
        }

        private List<Film> IndataSträngTillFilmLista(string filmListaSträng)
        {
            var ingaCitationsTecken = filmListaSträng.Replace("\"", string.Empty);
            var filmer = ingaCitationsTecken.Split(',');
            return filmer.Select(
                (t, i) => new Film { Id = i + 1, Namn = t }
                ).ToList();
        }
    }
}



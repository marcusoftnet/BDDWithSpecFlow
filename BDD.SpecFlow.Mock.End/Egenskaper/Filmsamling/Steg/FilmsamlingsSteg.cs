using System.Collections.Generic;
using System.IO;
using System.Linq;
using BDD.SpecFlow.Mock.End.Domain.Infrastructure;
using BDD.SpecFlow.Mock.End.Domain.Model;
using BDD.SpecFlow.Mock.End.Domain.Model.Entitet;
using BDD.SpecFlow.Mock.End.Domain.Vyer;
using Moq;
using TechTalk.SpecFlow;

namespace BDD.SpecFlow.Mock.End.Egenskaper.Filmsamling.Steg
{
    [Binding]
    public class FilmsamlingsSteg
    {
        private Domain.Model.Filmsamling _filmsamling;
        private FuskKommandoKälla _fuskKommandoKälla;
        private Mock<FilmRepository> _mockFilmRepository;
        private VyRepository _vyRepository;
        private Mock<TextWriter> _mockSystemOut;

        public FilmsamlingsSteg()
        {
            _mockSystemOut = new Mock<TextWriter>();

            _vyRepository = new MinnesVyRepository();
            _vyRepository.LäggTill(VyNamn.ANTAL_FILMER, new AntalFilmerVy(_mockSystemOut.Object));
            _vyRepository.LäggTill(VyNamn.LISTA_FILMER, new ListaFilmerVy(_mockSystemOut.Object));

            _mockFilmRepository = new Mock<FilmRepository>(MockBehavior.Strict);

            var mockSessionhelper = new Mock<SessionHelper>();

            _filmsamling = new Domain.Model.Filmsamling(_mockFilmRepository.Object, _vyRepository, mockSessionhelper.Object);

            _fuskKommandoKälla = new FuskKommandoKälla(_filmsamling);
        }

        [Given(@"att jag påbörjar min filmsamling från scratch")]
        public void Givet_Att_Jag_Påbörjar_Min_Filmsamling_Från_Scratch()
        {
            _mockFilmRepository.Setup(x => x.HämtaAlla()).Returns(new List<Film>());
        }

        [Given(@"att filmsamlingen innehåller (.+)$")]
        public void GivetAttFilmsamlingenInnehallerFilmLista(string filmListaSträng)
        {
            List<Film> testdataFilmLista = IndataSträngTillFilmLista(filmListaSträng);

            _mockFilmRepository.Setup(x => x.HämtaAlla()).Returns(testdataFilmLista);
        }

        [When(@"jag anger kommando: (.+)$")] // Regexp för vilket tecken som helst till slutet av raden
        public void När_Jag_Anger_Kommando(string kommando)
        {
            _fuskKommandoKälla.KörKommando(kommando);
        }

        [Then(@"ska resultatet vara: (.+)$")]
        public void Så_Ska_Resultatet_Vara(string resultat)
        {
            _mockSystemOut.Verify(x => x.WriteLine(resultat), Times.Once());
            _mockFilmRepository.Verify();
        }

        [Then(@"ska resultatet vara:")]
        public void Så_Ska_Resultatet_Vara(Table utdataRader)
        {
            _mockFilmRepository.Verify();
            
            // TODO: Fix to verify each call - don't know the Moq-syntax for that ... yet
            _mockSystemOut.Verify(x => x.WriteLine(utdataRader.Rows[0]["Rad"]), Times.Once());

            //foreach (var row in utdataRader.Rows)
            //{
            //    var utdataRad = row["Rad"];
            //    _mockSystemOut.Verify(x => x.WriteLine(utdataRad), Times.Once());
            //}
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


    public class FuskKommandoKälla : KommandoKälla
    {
        private readonly Domain.Model.Filmsamling _filmsamling;

        public FuskKommandoKälla(Domain.Model.Filmsamling filmsamling)
        {
            _filmsamling = filmsamling;
        }

        public void KörKommando(string kommando)
        {
            if (kommando == "AntalFilmer")
            {
                _filmsamling.AntalFilmer();
            }

            if (kommando == "ListaFilmer")
            {
                _filmsamling.ListaFilmer();
            }
        }
    }

}



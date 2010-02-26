using System.Collections.Generic;
using System.IO;
using BDD.SpecFlow.Mock.Start.Domain.Infrastructure;
using BDD.SpecFlow.Mock.Start.Domain.Model;
using BDD.SpecFlow.Mock.Start.Domain.Model.Entitet;
using BDD.SpecFlow.Mock.Start.Domain.Vyer;
using Moq;
using TechTalk.SpecFlow;

namespace BDD.SpecFlow.Mock.Start.Egenskaper.Filmsamling.Steg
{
    [Binding]
    public class FilmsamlingsSteg
    {
        private Domain.Model.Filmsamling _filmsamling;
        private FuskKommandoKälla _fuskKommandoKälla;
        private Mock<FilmRepository> _mockFilmRepository;
        private VyRepository _vyRepository;
        private Mock<TextWriter> _mockSystemOut;


        [Given(@"att jag påbörjar min filmsamling från scratch")]
        public void Givet_Att_Jag_Påbörjar_Min_Filmsamling_Från_Scratch()
        {
            _mockSystemOut = new Mock<TextWriter>();

            _vyRepository = new MinnesVyRepository();
            var antalFilmerVy = new AntalFilmerVy(_mockSystemOut.Object);
            _vyRepository.LäggTill(VyNamn.ANTAL_FILMER, antalFilmerVy);

            _mockFilmRepository = new Mock<FilmRepository>(MockBehavior.Strict);
            _mockFilmRepository.Setup(x => x.HämtaAlla())
                .Returns(new List<Film>())
                .AtMostOnce();

            var mockSessionhelper = new Mock<SessionHelper>();
            
            _filmsamling = new Domain.Model.Filmsamling(_mockFilmRepository.Object, _vyRepository, mockSessionhelper.Object); 

            _fuskKommandoKälla = new FuskKommandoKälla(_filmsamling);
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
        }
    }
}



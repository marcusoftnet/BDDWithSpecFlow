using System.Collections.Generic;
using System.IO;
using BDD.SpecFlow.Domain.Infrastructure;
using BDD.SpecFlow.Domain.Model;
using BDD.SpecFlow.Domain.Model.Entitet;
using BDD.SpecFlow.Domain.Vyer;
using Moq;
using TechTalk.SpecFlow;

namespace BDD.SpecFlow.Specs.Egenskaper.Filmsamling.Steg
{
    [Binding]
    public class FilmsamlingsSteg
    {
        private global::BDD.SpecFlow.Domain.Model.Filmsamling filmsamling;
        private FuskKommandoKälla _fuskKommandoKälla;
        private Vy _vy;
        private FilmRepository filmRepository;
        private VyRepository vyRepository;
        private Mock<TextWriter> _mockSystemOut;

        [Given(@"att jag påbörjar min filmsamling från scratch")]
        public void Givet_Att_Jag_Påbörjar_Min_Filmsamling_Från_Scratch()
        {
            _vy = new FuskVy();
            _mockSystemOut = new Mock<TextWriter>();

            vyRepository = new MinnesVyRepository();
            var antalFilmerVy = new AntalFilmerVy(_mockSystemOut.Object);
            vyRepository.LäggTill(VyNamn.ANTAL_FILMER, antalFilmerVy);

            filmRepository = new FuskFilmRepository();

            var mockSessionhelper = new Mock<SessionHelper>();
            
            filmsamling = new global::BDD.SpecFlow.Domain.Model.Filmsamling(filmRepository, vyRepository, mockSessionhelper.Object); 

            _fuskKommandoKälla = new FuskKommandoKälla(filmsamling);
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
        }
    }

    public class FuskFilmRepository : FilmRepository
    {
        public IList<Film> HämtaAlla()
        {
            return new List<Film>();
        }
    }

    public class FuskKommandoKälla : KommandoKälla
    {
        private readonly global::BDD.SpecFlow.Domain.Model.Filmsamling _filmsamling;

        public FuskKommandoKälla(global::BDD.SpecFlow.Domain.Model.Filmsamling filmsamling)
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

    public class FuskVy : Vy
    {
        public string Meddelande { get; private set; }
    }
}



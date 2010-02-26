using BDD.SpecFlow.Mock.Start.Domain.Infrastructure;
using BDD.SpecFlow.Mock.Start.Domain.Vyer;

namespace BDD.SpecFlow.Mock.Start.Domain.Model
{
    public class Filmsamling
    {
        private readonly FilmRepository _filmRepository;
        private readonly VyRepository _vyRepository;
        private readonly SessionHelper _sessionHelper;

        public Filmsamling(FilmRepository filmRepository, VyRepository vyRepository, SessionHelper sessionHelper)
        {
            _filmRepository = filmRepository;
            _vyRepository = vyRepository;
            _sessionHelper = sessionHelper;
        }

        public void AntalFilmer()
        {
            // starta session
            _sessionHelper.StartSession();
            
            var vy = _vyRepository.HämtaVy<AntalFilmerVy>(VyNamn.ANTAL_FILMER);

            vy.Presentera(_filmRepository.HämtaAlla().Count);

            _sessionHelper.CloseSession();
        }
    }
}



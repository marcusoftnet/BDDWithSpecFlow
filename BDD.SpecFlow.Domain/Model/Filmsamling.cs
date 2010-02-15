using BDD.SpecFlow.Domain.Vyer;

namespace BDD.SpecFlow.Domain.Model
{
    public class Filmsamling
    {
        private readonly FilmRepository _filmRepository;
        private readonly VyRepository _vyRepository;

        public Filmsamling(FilmRepository filmRepository, VyRepository vyRepository)
        {
            _filmRepository = filmRepository;
            _vyRepository = vyRepository;
        }

        public void AntalFilmer()
        {
            var vy = _vyRepository.HämtaVy<AntalFilmerVy>(VyNamn.ANTAL_FILMER);

            vy.Presentera(_filmRepository.HämtaAlla().Count);
        }
    }
}



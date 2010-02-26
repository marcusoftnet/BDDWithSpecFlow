using System;
using BDD.SpecFlow.IoC.End.Domain.Vyer;
using NUnit.Framework;

namespace BDD.SpecFlow.IoC.End.Design
{
    [TestFixture]
    public class VyRepositoryTests
    {
        private MinnesVyRepository _repository;


        private class TestView : Vy
        {
            public string Meddelande
            {
                get { return string.Empty; }
            }
        }

        [Test]
        public void ska_kunna_hämta_vy_av_en_viss_typ()
        {
            // Arrange
            _repository = new MinnesVyRepository();
            _repository.LäggTill(VyNamn.ANTAL_FILMER, new AntalFilmerVy(System.Console.Out));

            // act
            var vyFrånRepository = _repository.HämtaVy<AntalFilmerVy>(VyNamn.ANTAL_FILMER);

            // Assert
            Assert.That(vyFrånRepository, Is.InstanceOf(typeof(AntalFilmerVy)));
        }

        [Test, ExpectedException(typeof(ApplicationException))]
        public void ska_kasta_application_exception_om_repository_är_tomt()
        {
            // Arrange DRY
            _repository = new MinnesVyRepository();

            // act
            var vyFrånRepository = _repository.HämtaVy<AntalFilmerVy>(VyNamn.ANTAL_FILMER);

        }

        [Test, ExpectedException(typeof(ApplicationException))]
        public void ska_kasta_application_exception_om_man_försöker_hämta_vy_av_fel_typ()
        {
            // Arrange
            _repository = new MinnesVyRepository();
            _repository.LäggTill(VyNamn.ANTAL_FILMER, new AntalFilmerVy(System.Console.Out));

            // act
            var vyfrånRepository = _repository.HämtaVy<TestView>(VyNamn.ANTAL_FILMER);
        }
    }
}



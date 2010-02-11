using System.IO;
using BDD.SpecFlow.Domain.Vyer;
using Moq;
using NUnit.Framework;

namespace BDD.SpecFlow.Tests
{
    [TestFixture]
    public class AntalFilmerVyFörTomFilmsamlingExempel
    {
        [Test]
        public void ska_presentera_antalet_i_text()
        {
            // Arrange
            var _mockSystemOut = new Mock<TextWriter>();
            var vy = new AntalFilmerVy(_mockSystemOut.Object);

            // Act
            vy.Presentera(0);

            // Assert
            _mockSystemOut.Verify(x => x.WriteLine("Du har inga filmer i samlingen"), Times.Once());
        }
    }
}



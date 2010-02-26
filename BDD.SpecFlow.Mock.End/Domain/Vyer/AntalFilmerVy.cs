using System.IO;

namespace BDD.SpecFlow.Mock.End.Domain.Vyer
{
    public class AntalFilmerVy : Vy
    {
        private readonly TextWriter _textWriter;

        public AntalFilmerVy(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public string Meddelande
        {
            get { return string.Empty; }
        }

        public void Presentera(int antalFilmer)
        {
            _textWriter.WriteLine("Du har inga filmer i samlingen");
        }
    }
}
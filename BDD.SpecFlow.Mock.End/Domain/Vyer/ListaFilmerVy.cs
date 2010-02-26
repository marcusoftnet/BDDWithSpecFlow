using System.Collections.Generic;
using System.IO;
using BDD.SpecFlow.Mock.End.Domain.Model.Entitet;

namespace BDD.SpecFlow.Mock.End.Domain.Vyer
{
    public class ListaFilmerVy : Vy
    {
        private readonly TextWriter _textWriter;

        public ListaFilmerVy(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public string Meddelande
        {
            get { return string.Empty; }
        }

        public void Presentera(IList<Film> filmLista)
        {
            foreach (var film in filmLista)
            {
                _textWriter.WriteLine(film.Namn);
            }

            _textWriter.WriteLine("Du har {0} filmer i samlingen", filmLista.Count);
        }

    }
}
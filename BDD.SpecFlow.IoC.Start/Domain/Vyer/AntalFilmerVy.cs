﻿using System.IO;

namespace BDD.SpecFlow.IoC.Start.Domain.Vyer
{
    public class AntalFilmerVy : Vy
    {
        private readonly TextWriter _textWriter;

        public AntalFilmerVy(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        
        public void Presentera(int antalFilmer)
        {
            _textWriter.WriteLine("Du har inga filmer i samlingen");
        }
    }
}
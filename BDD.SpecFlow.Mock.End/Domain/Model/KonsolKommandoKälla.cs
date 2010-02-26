namespace BDD.SpecFlow.Mock.End.Domain.Model
{
    public class KonsolKommandoKälla : KommandoKälla
    {
        private readonly Filmsamling _filmsamling;

        public KonsolKommandoKälla(Filmsamling filmsamling)
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
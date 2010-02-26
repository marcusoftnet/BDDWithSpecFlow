using System;
using System.Collections.Generic;

namespace BDD.SpecFlow.IoC.End.Domain.Vyer
{
    public class MinnesVyRepository :  VyRepository
    {
        private Dictionary<VyNamn, Vy> vyDicionary;

        public MinnesVyRepository()
        {
            vyDicionary = new Dictionary<VyNamn, Vy>();
        }

        public void LäggTill(VyNamn vyNamn, Vy vy)
        {
            vyDicionary.Add(vyNamn, vy);
        }

        public T HämtaVy<T>(VyNamn vyNamn) where T : Vy
        {
            if(!vyDicionary.ContainsKey(vyNamn))
            {
                throw new ApplicationException(string.Format("{0} finns inte bland registrerade vyer", vyNamn)); 
            }

            if(!(vyDicionary[vyNamn] is T))
            {
                throw new ApplicationException(string.Format("{0} är inte av typen {1}", vyNamn, typeof(T).Name));                 
            }

            return (T)vyDicionary[vyNamn];
        }
    }

}
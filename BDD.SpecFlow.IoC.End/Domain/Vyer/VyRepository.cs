﻿using StructureMap;

namespace BDD.SpecFlow.IoC.End.Domain.Vyer
{
    [PluginFamily(IsSingleton = true)]
    public interface VyRepository
    {
        void LäggTill(VyNamn vyNamn, Vy vy);
        T HämtaVy<T>(VyNamn vyNamn) where T : Vy;
    }

    public enum VyNamn
    {
        ANTAL_FILMER,
        LISTA_FILMER
    }
}
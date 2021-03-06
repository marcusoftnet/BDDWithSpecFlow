﻿using StructureMap;

namespace BDD.SpecFlow.Mock.Start.Domain.Vyer
{
    [PluginFamily(IsSingleton = true)]
    public interface VyRepository
    {
        void LäggTill(VyNamn vyNamn, Vy vy);
        T HämtaVy<T>(VyNamn vyNamn) where T : Vy;
    }

    public enum VyNamn
    {
        ANTAL_FILMER
    }
}
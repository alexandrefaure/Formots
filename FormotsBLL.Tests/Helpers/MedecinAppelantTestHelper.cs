using System;
using FormotsCommon.DTO;

namespace FormotsBLL.Tests.Helpers
{
    public sealed class MedecinAppelantTestHelper
    {
        public static MedecinAppelantDto GetFakeMedecinAppelantDto()
        {
            return new MedecinAppelantDto
            {
                Nom = "Sauvignon",
                Prenom = "Laurent",
                Adresse = "1 allée Charles Malpel - 31300 Toulouse",
                DateNaissance = new DateTime(1987, 1, 1),
                Email = "l.sauvignon@gmail.com",
                Genre = true,
                NumeroTelephoneFixe = "0555554741",
                NumeroTelephonePortable = "0671554741",
                TiersNom = "Pérez",
                TiersLienParente = "Père",
                TiersEmail = "j.perez@omp.fr",
                TiersTelephone = "0552222369"
            };
        }
    }
}

using System;

namespace FormotsCommon.Helper
{
    public static class MedecinAppelantHelper
    {
        public static int? GetAge(DateTime? dateNaissance)
        {
            if (dateNaissance == null)
            {
                return null;
            }

            var todayDate = DateTime.Today.Year;
            var currentMaDateNaissance = dateNaissance.Value.Year;
            return todayDate - currentMaDateNaissance;
        }
    }
}

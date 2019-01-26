using MahApps.Metro.Controls.Dialogs;

namespace FormotsCommon.Utils
{
    public static class MainDialogManager
    {
        public static MetroDialogSettings GetOuiNonAnnulerDialogSettings()
        {
            return new MetroDialogSettings()
            {
                AffirmativeButtonText = "Oui",
                NegativeButtonText = "Non",
                FirstAuxiliaryButtonText = "Annuler",
                ColorScheme = MetroDialogColorScheme.Accented
            };
        }

        public static MetroDialogSettings GetOkDialogSettings(MetroDialogColorScheme colorScheme = MetroDialogColorScheme.Accented)
        {
            return new MetroDialogSettings()
            {
                AffirmativeButtonText = "Ok",
                ColorScheme = colorScheme
            };
        }
    }
}

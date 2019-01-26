using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using FormotsCommon;
using FormotsCommon.DTO;

namespace FormotsGUI.ViewModels.Formulaires
{
    public class FormulaireCfpEditFormViewModel : FormulaireBaseViewModel
    {
        public List<KeyValuePair<int, string>> ActivitesLoisirsList => GetActivitesLoisirsList();

        public static List<KeyValuePair<int, string>> GetActivitesLoisirsList()
        {
            var activitesLoisirs = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Sport occasionnel"),
                new KeyValuePair<int, string>(2, "Sport régulier (1 à 3x/semaine)"),
                new KeyValuePair<int, string>(3, "Loisir non sportifs"),
                new KeyValuePair<int, string>(4, "Pas d’activité de loisirs")
            };
            return activitesLoisirs;
        }

        public List<KeyValuePair<int, string>> ExerciceProfessionnelList => FormulaireReferential.GetExerciceProfessionnelList();

        public List<KeyValuePair<int, string>> FonctionHospitaliereList => GetFonctionHospitaliereList();
        public static List<KeyValuePair<int, string>> GetFonctionHospitaliereList()
        {
            var fonctionHospitaliereList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Chef de clinique ou CCA"),
                new KeyValuePair<int, string>(2, "PH"),
                new KeyValuePair<int, string>(3, "PU-PH"),
                new KeyValuePair<int, string>(4, "PH contractuel"),
                new KeyValuePair<int, string>(5, "Autre")
            };
            return fonctionHospitaliereList;
        }

        public List<KeyValuePair<int, string>> PrecisionsExerciceProfessionnelList => FormulaireReferential.GetPrecisionsExerciceProfessionnelList();

        public List<KeyValuePair<int, string>> SpecialiteList => FormulaireReferential.GetSpecialiteList();

        public List<KeyValuePair<int, string>> StatutCivilList => FormulaireReferential.GetStatutCivilList();

        public List<KeyValuePair<int, string>> NombreHeuresHebdoMoyenList => FormulaireReferential.GetNombreHeuresHebdoMoyenList();

        public List<KeyValuePair<int, string>> CongesAnnuelsList => FormulaireReferential.GetCongesAnnuelsList();

        public FormulaireCfpEditFormViewModel()
        {
            _instance = this;
        }

        public override void Initialize()
        {
            ((FormulaireCfpDto)Formulaire).IsPrecisionExerciceProfessionnelVisible = Visibility.Collapsed;
            ((FormulaireCfpDto)Formulaire).IsFonctionHospitaliereVisible = Visibility.Collapsed;
        }
    }
}
using System.Collections.Generic;
using System.Windows;
using FormotsCommon;
using FormotsCommon.DTO;

namespace FormotsGUI.ViewModels.Formulaires
{
    public class FormulaireSeoEditFormViewModel : FormulaireBaseViewModel
    {
        public List<KeyValuePair<int, string>> TypeEntretienList => FormulaireReferential.GetTypeEntretienList();

        public List<KeyValuePair<int, string>> DureeEntretienList => GetDureeEntretienList();
        public static List<KeyValuePair<int, string>> GetDureeEntretienList()
        {
            var dureeEntretienList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "< 30 min"),
                new KeyValuePair<int, string>(2, "30 min. à 1H"),
                new KeyValuePair<int, string>(3, "Entre 1H et 1H30"),
                new KeyValuePair<int, string>(4, "> 1H30")
            };
            return dureeEntretienList;
        }

        public List<KeyValuePair<int, string>> DureeDeplacementList => GetDureeDeplacementList();
        public static List<KeyValuePair<int, string>> GetDureeDeplacementList()
        {
            var dureeDeplacementList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Aucun déplacement"),
                new KeyValuePair<int, string>(2, "< 30 min. à 1H"),
                new KeyValuePair<int, string>(3, "Entre 1H et 1H30"),
                new KeyValuePair<int, string>(4, "> 1H30")
            };
            return dureeDeplacementList;
        }

        public List<KeyValuePair<int, string>> RechercheDocumentationList => GetRechercheDocumentationList();
        public static List<KeyValuePair<int, string>> GetRechercheDocumentationList()
        {
            var rechercheDocumentationList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Aucune recherche de doc"),
                new KeyValuePair<int, string>(2, "< 30 min. à 1H"),
                new KeyValuePair<int, string>(3, "Entre 1H et 1H30"),
                new KeyValuePair<int, string>(4, "> 1H30")
            };
            return rechercheDocumentationList;
        }

        public List<KeyValuePair<int, string>> ContactsExternesMotsList => GetContactsExternesMotsList();
        public static List<KeyValuePair<int, string>> GetContactsExternesMotsList()
        {
            var contactsExternesMotsList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Aucun contact externe"),
                new KeyValuePair<int, string>(2, "< 30 min. à 1H"),
                new KeyValuePair<int, string>(3, "Entre 1H et 1H30"),
                new KeyValuePair<int, string>(4, "> 1H30")
            };
            return contactsExternesMotsList;
        }

        public List<KeyValuePair<int, string>> AppelContactInterneMotsList => FormulaireReferential.GetAppelContactInterneMotsList();

        public List<KeyValuePair<int, string>> OrientationsList => FormulaireReferential.GetOrientationsList();

        public FormulaireSeoEditFormViewModel()
        {
            _instance = this;
        }

        public override void Initialize()
        {
            ((FormulaireSeoDto)Formulaire).IsTravailConnexeEntretienPanelVisible = Visibility.Collapsed;
            ((FormulaireSeoDto)Formulaire).IsMotifAppelContactInterneMotsVisible = Visibility.Collapsed;
            ((FormulaireSeoDto)Formulaire).IsOrientationAutreVisible = Visibility.Collapsed;
        }
    }
}
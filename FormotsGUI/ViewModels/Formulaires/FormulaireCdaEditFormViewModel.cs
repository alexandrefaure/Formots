using System.Collections.Generic;
using System.Windows;
using FormotsCommon;
using FormotsCommon.DTO;

namespace FormotsGUI.ViewModels.Formulaires
{
    public class FormulaireCdaEditFormViewModel : FormulaireBaseViewModel
    {
        public List<KeyValuePair<int, string>> MotifAppelInitialMaList => FormulaireReferential.GetMotifAppelInitialMaList();

        public List<KeyValuePair<int, string>> TypeProblematiqueComplementaireList => FormulaireReferential.GetTypeProblematiqueComplementaireList();

        public List<KeyValuePair<int, string>> AddictionsList => FormulaireReferential.GetAddictionsList();

        public List<KeyValuePair<int, string>> AntecedentsPersonnelsList => GetAntecedentsPersonnelsList();
        public static List<KeyValuePair<int, string>> GetAntecedentsPersonnelsList()
        {
            var _antecedentsPersonnelsList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Cardio-vasculaire"),
                new KeyValuePair<int, string>(2, "Rhumatologique ou ostéo-articulaire"),
                new KeyValuePair<int, string>(3, "Neurologique"),
                new KeyValuePair<int, string>(4, "Oncologique"),
                new KeyValuePair<int, string>(5, "Inflammatoire ou maladie de système"),
                new KeyValuePair<int, string>(6, "Psychique ou psychiatrique"),
                new KeyValuePair<int, string>(7, "Pathologie chronique autre(Diabète, HTA, pb nephro…)"),
                new KeyValuePair<int, string>(8, "ATCD de tentative d’autolyse(TA)"),
            };
            return _antecedentsPersonnelsList;
        }

        public FormulaireCdaEditFormViewModel()
        {
            _instance = this;
        }

        public override void Initialize()
        {
            ((FormulaireCdaDto)Formulaire).IsAutreMotifAppelVisible = Visibility.Collapsed;
            ((FormulaireCdaDto)Formulaire).IsAutreProblematiqueVisible = Visibility.Collapsed;
            ((FormulaireCdaDto)Formulaire).IsAddictionsVisible = Visibility.Collapsed;
        }
    }
}
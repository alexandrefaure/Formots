using System.Collections.Generic;
using System.Windows;
using FormotsCommon;
using FormotsCommon.DTO;

namespace FormotsGUI.ViewModels.Formulaires
{
    public class FormulaireActEditFormViewModel : FormulaireBaseViewModel
    {
        public FormulaireActEditFormViewModel()
        {
            _instance = this;
        }

        public override void Initialize()
        {
            ((FormulaireActDto) Formulaire).IsAutreSuggestionTextBoxVisible = Visibility.Collapsed;
        }

        public List<KeyValuePair<int, string>> SuggestionAppelMotsList => FormulaireReferential.GetSuggestionAppelMotsList();
        public List<KeyValuePair<int, string>> ContactParList => FormulaireReferential.GetContactParList();
    }
}
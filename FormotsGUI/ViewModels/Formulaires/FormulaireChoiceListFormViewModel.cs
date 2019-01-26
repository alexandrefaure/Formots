using System.Collections.ObjectModel;
using System.Windows.Input;
using FormotsCommon;
using FormotsCommon.Utils;

namespace FormotsGUI.ViewModels.Formulaires
{
    public class FormulaireChoiceListFormViewModel : BaseViewModel
    {
        private static FormulaireChoiceListFormViewModel _instance = new FormulaireChoiceListFormViewModel();

        public static FormulaireChoiceListFormViewModel Instance => _instance;

        public FormulaireChoiceListFormViewModel()
        {
            FormulaireTypesList = new ObservableCollection<FormulaireType>
            {
                FormulaireType.ACcueilTelephonique,
                FormulaireType.ContexteFamilialProfessionnel,
                FormulaireType.ContexteDemandeAccompagnement,
                FormulaireType.EvaluationConsequenceVecu,
                FormulaireType.SuiviEntretiensOrientations,
                FormulaireType.BilanSyntheseFinAccompagnement
            };
        }

        private ObservableCollection<FormulaireType> _formulaireTypesList;
        public ObservableCollection<FormulaireType> FormulaireTypesList
        {
            get => _formulaireTypesList;
            set
            {
                if (Equals(value, _formulaireTypesList))
                {
                    return;
                }

                _formulaireTypesList = value;
                OnPropertyChanged("FormulaireTypesList");
            }
        }

        private FormulaireType _selectedFormulaireType;
        public FormulaireType SelectedFormulaireType
        {
            get => _selectedFormulaireType;
            set
            {
                if (Equals(value, _selectedFormulaireType))
                {
                    return;
                }

                _selectedFormulaireType = value;
                OnPropertyChanged("SelectedFormulaireType");
            }
        }

        #region Commands

        private ICommand _addFormulaireTypeCommand;

        public ICommand AddFormulaireTypeCommand
        {
            get
            {
                return _addFormulaireTypeCommand ?? (_addFormulaireTypeCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = FormulaireBaseViewModel.Instance.AddFormulaireType });
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using FormotsBLL.BLL;
using FormotsCommon;
using FormotsCommon.DTO;
using FormotsCommon.Utils;
using FormotsGUI.Pages.Dossier;
using FormotsGUI.Pages.Formulaires;
using FormotsGUI.ViewModels.Dossiers;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace FormotsGUI.ViewModels.Formulaires
{
    public class FormulaireBaseViewModel : BaseViewModel
    {
        protected static FormulaireBaseViewModel _instance = new FormulaireBaseViewModel();
        private FormulairesBLL _formulaireBll;

        public static FormulaireBaseViewModel Instance
        {
            get
            {
                return _instance;
            }
        }

        public FormulaireBaseViewModel()
        {
            _formulaireBll = new FormulairesBLL();
            _dialogService = new DialogService();
        }

        public void InitializeBase()
        {
            Initialize();
            Formulaire.DtCreation = DateTime.Now;
        }

        public virtual void Initialize()
        {
        }

        public static DossierDto CurrentDossierDto
        {
            get
            {
                return DossierEditFormViewModel.Instance.DossierDtoToAddOrUpdate;
            }
        }

        public ObservableCollection<FormulaireDto> AllFormulairesDtoList
        {
            get
            {
                return _formulaireBll.GetFormulairesList();
            }
        }

        public void AddFormulaireType(object obj)
        {
            FormulaireChoiceListFormViewModel.Instance.OnClosingRequest();
            if (FormulaireChoiceListFormViewModel.Instance.SelectedFormulaireType.Equals(FormulaireType
                .ACcueilTelephonique))
            {
                EditFormulaire<FormulaireActDto, FormulaireActEditFormViewModel, FormulaireActEditForm>(null);
            }
            else if (FormulaireChoiceListFormViewModel.Instance.SelectedFormulaireType.Equals(FormulaireType
                .ContexteFamilialProfessionnel))
            {
                EditFormulaire<FormulaireCfpDto, FormulaireCfpEditFormViewModel, FormulaireCfpEditForm>(null);
            }
            else if (FormulaireChoiceListFormViewModel.Instance.SelectedFormulaireType.Equals(FormulaireType
                .ContexteDemandeAccompagnement))
            {
                EditFormulaire<FormulaireCdaDto, FormulaireCdaEditFormViewModel, FormulaireCdaEditForm>(null);
            }
            else if (FormulaireChoiceListFormViewModel.Instance.SelectedFormulaireType.Equals(FormulaireType
                .EvaluationConsequenceVecu))
            {
                EditFormulaire<FormulaireEcvDto, FormulaireEcvEditFormViewModel, FormulaireEcvEditForm>(null);
            }
            else if (FormulaireChoiceListFormViewModel.Instance.SelectedFormulaireType.Equals(FormulaireType
                .SuiviEntretiensOrientations))
            {
                EditFormulaire<FormulaireSeoDto, FormulaireSeoEditFormViewModel, FormulaireSeoEditForm>(null);
            }
            else if (FormulaireChoiceListFormViewModel.Instance.SelectedFormulaireType.Equals(FormulaireType
                .BilanSyntheseFinAccompagnement))
            {
                EditFormulaire<FormulaireSfaDto, FormulaireSfaEditFormViewModel, FormulaireSfaEditForm>(null);
            }
            DossierEditFormViewModel.Instance.AsynchroneUpdateList();
        }

        protected virtual void SaveFormulaire(object obj)
        {
            BaseSaveObject("le formulaire", Formulaire, IsNewFormulaire, $"{Formulaire.Type.Code} ({Formulaire.DtCreation})",
                Formulaire => _formulaireBll.AddOrUpdateFormulaire(Formulaire),DossierEditFormViewModel.Instance);
            OnClosingRequest();
        }

        public void EditFormulaire()
        {
            if (Formulaire == null)
            {
                return;
            }

            if (Formulaire.Type.Equals(FormulaireType.ACcueilTelephonique))
            {
                EditFormulaire<FormulaireActDto, FormulaireActEditFormViewModel, FormulaireActEditForm>((FormulaireActDto)Formulaire);
            }
            if (Formulaire.Type.Equals(FormulaireType.ContexteFamilialProfessionnel))
            {
                EditFormulaire<FormulaireCfpDto, FormulaireCfpEditFormViewModel, FormulaireCfpEditForm>((FormulaireCfpDto)Formulaire);
            }
            if (Formulaire.Type.Equals(FormulaireType.ContexteDemandeAccompagnement))
            {
                EditFormulaire<FormulaireCdaDto, FormulaireCdaEditFormViewModel, FormulaireCdaEditForm>((FormulaireCdaDto)Formulaire);
            }
            if (Formulaire.Type.Equals(FormulaireType.EvaluationConsequenceVecu))
            {
                EditFormulaire<FormulaireEcvDto, FormulaireEcvEditFormViewModel, FormulaireEcvEditForm>((FormulaireEcvDto)Formulaire);
            }
            if (Formulaire.Type.Equals(FormulaireType.SuiviEntretiensOrientations))
            {
                EditFormulaire<FormulaireSeoDto, FormulaireSeoEditFormViewModel, FormulaireSeoEditForm>((FormulaireSeoDto)Formulaire);
            }
            if (Formulaire.Type.Equals(FormulaireType.BilanSyntheseFinAccompagnement))
            {
                EditFormulaire<FormulaireSfaDto, FormulaireSfaEditFormViewModel, FormulaireSfaEditForm>((FormulaireSfaDto)Formulaire);
            }
        }

        public void DeleteFormulaire()
        {
            BaseDeleteObject("le formulaire", Formulaire, Formulaire.Id.ToString(),
                Formulaire => _formulaireBll.DeleteFormulaire(Formulaire));
        }

        public static void EditFormulaire<T, V, F>(T formulaireDto) where T : FormulaireDto where V : FormulaireBaseViewModel where F : MetroWindow
        {
            var formulaireViewModel = (V)Activator.CreateInstance(typeof(V), new object[] { });
            if (formulaireDto == null)
            {
                var formulaireTypeDto = (T)Activator.CreateInstance(typeof(T), new object[] { });
                formulaireTypeDto.IdDossier = CurrentDossierDto.Id;
                Instance.Formulaire = formulaireTypeDto;
                Instance.IsNewFormulaire = true;
                Instance.InitializeBase();
            }
            else
            {
                Instance.IsNewFormulaire = false;
                Instance.Formulaire = formulaireDto;
            }

            WindowHelper.ShowDialogWindow<F>();
        }

        protected virtual void CancelFormulaire(object obj)
        {
            OnClosingRequest();
        }

        protected virtual void BaseSaveFormulaire(object obj)
        {
            SaveFormulaire(obj);
        }

        protected virtual void BaseCancelFormulaire(object obj)
        {
            CancelFormulaire(obj);
        }

        private FormulaireDto _formulaire;
        public virtual FormulaireDto Formulaire
        {
            get => _formulaire;
            set
            {
                if (Equals(value, _formulaire))
                {
                    return;
                }

                _formulaire = value;
                OnPropertyChanged("Formulaire");
            }
        }

        private bool _isNewFormulaire;
        public bool IsNewFormulaire
        {
            get => _isNewFormulaire;
            set
            {
                if (Equals(value, _isNewFormulaire))
                {
                    return;
                }

                _isNewFormulaire = value;
                OnPropertyChanged("IsNewFormulaire");
            }
        }

        #region BackgroundWorker List

        protected override object DoWork(BackgroundWorker bgworker)
        {
            DossierEditFormViewModel.Instance.AsynchroneUpdateList();
            return null;
        }

        protected override void ProgressChanged(ProgressChangedEventArgs e, object list)
        {
        }

        protected override void RunWorkerCompleted()
        {
        }

        #endregion

        public override string WindowTitle => string.Concat("Formulaire ", Formulaire.Type.Libelle, " - ", Formulaire.Id, $" (Dossier {CurrentDossierDto.NumeroAnonymatGlobal})");

        #region Commands

        private ICommand _saveFormulaireCommand;

        public ICommand SaveFormulaireCommand
        {
            get
            {
                return _saveFormulaireCommand ?? (_saveFormulaireCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = BaseSaveFormulaire });
            }
        }

        private ICommand _cancelFormulaireCommand;
        private DialogService _dialogService;

        public ICommand CancelFormulaireCommand
        {
            get
            {
                return _cancelFormulaireCommand ?? (_cancelFormulaireCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = BaseCancelFormulaire });
            }
        }

        #endregion

    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FormotsBLL.BLL;
using FormotsCommon.DTO;
using FormotsCommon.Utils;
using FormotsGUI.Pages.Formulaires;
using FormotsGUI.Pages.MedecinAppelant;
using FormotsGUI.ViewModels.Formulaires;
using FormotsGUI.ViewModels.MedecinAppelants;
using MahApps.Metro.Controls.Dialogs;

namespace FormotsGUI.ViewModels.Dossiers
{
    public class DossierEditFormViewModel : BaseViewModel
    {
        private readonly DialogService _dialogService;
        private readonly DossiersBLL _dossiersBll;
        private DossierDto _dossierDtoToAddOrUpdate;
        private Visibility _isSaveButtonVisible;
        private string _tiersAppelantExpanderTitle;

        private static DossierEditFormViewModel _instance = new DossierEditFormViewModel();

        public static DossierEditFormViewModel Instance
        {
            get
            {
                return _instance;
            }
        }

        public DossierEditFormViewModel()
        {
            _dialogService = new DialogService();
            _dossiersBll = new DossiersBLL();
            _formulairesBll = new FormulairesBLL();
            AsynchroneUpdateList();
        }

        public Visibility IsSaveButtonVisible
        {
            get => _isSaveButtonVisible;
            set
            {
                if (Equals(value, _isSaveButtonVisible))
                {
                    return;
                }

                _isSaveButtonVisible = value;
                OnPropertyChanged("IsSaveButtonVisible");
            }
        }

        public DossierDto DossierDtoToAddOrUpdate
        {
            get => _dossierDtoToAddOrUpdate;
            set
            {
                if (Equals(value, _dossierDtoToAddOrUpdate))
                {
                    return;
                }

                _dossierDtoToAddOrUpdate = value;
                OnPropertyChanged("DossierDtoToAddOrUpdate");
            }
        }

        private FormulaireDto _selectedFormulaire;
        public FormulaireDto SelectedFormulaire
        {
            get => _selectedFormulaire;
            set
            {
                if (Equals(value, _selectedFormulaire))
                {
                    return;
                }

                _selectedFormulaire = value;
                OnPropertyChanged("SelectedFormulaire");
            }
        }

        private ObservableCollection<FormulaireDto> _formulairesList;
        public ObservableCollection<FormulaireDto> FormulairesList
        {
            get { return _formulairesList; }
            set
            {
                if (_formulairesList == value)
                {
                    return;
                }

                _formulairesList = value;
                OnPropertyChanged("FormulairesList");
            }
        }

        #region BackgroundWorker List

        protected override object DoWork(BackgroundWorker bgworker)
        {
            var i = 0;
            var asyncFormulairesList = new ObservableCollection<FormulaireDto>();
            foreach (var formulaire in _formulairesBll.GetFormulairesListByDossierId(DossierDtoToAddOrUpdate.Id).OrderBy(f=>f.DtCreation))
            {
                asyncFormulairesList.Add(formulaire);
                i++;
            }
            bgworker?.ReportProgress(i, asyncFormulairesList);
            return asyncFormulairesList;
        }

        protected override void ProgressChanged(ProgressChangedEventArgs e, object list)
        {
            ObservableCollection<FormulaireDto> partialresult;
            if (e == null && list != null)
            {
                partialresult = (ObservableCollection<FormulaireDto>)list;
            }
            else
            {
                partialresult = (ObservableCollection<FormulaireDto>)e.UserState;
            }

            FormulairesList = new ObservableCollection<FormulaireDto>();
            foreach (var formulaireDto in partialresult)
            {
                FormulairesList.Add(formulaireDto);
                DossierDtoToAddOrUpdate.FormulairesCount = FormulairesList.Count;
                OnPropertyChanged("WindowTitle");
            }
        }

        protected override void RunWorkerCompleted()
        {
            OnPropertyChanged("FormulairesList");
        }

        #endregion

        public string TiersAppelantExpanderTitle
        {
            get => _tiersAppelantExpanderTitle;
            set
            {
                if (Equals(value, _tiersAppelantExpanderTitle))
                {
                    return;
                }

                _tiersAppelantExpanderTitle = value;
                OnPropertyChanged("TiersAppelantExpanderTitle");
            }
        }

        public override string WindowTitle
        {
            get => GetWindowTitle(DossierDtoToAddOrUpdate, DossierDtoToAddOrUpdate.FormulairesCount);
        }

        private static string GetWindowTitle(DossierDto selectedDossier, int formulairesCount)
        {
            if (selectedDossier != null)
            {
                return $"Dossier {selectedDossier.MedecinAppelantDto.NumeroAnonymatGlobal} ({formulairesCount} formulaires)";
            }
            return "Nouveau dossier";
        }

        protected void SaveDossier(object obj)
        {
            var medecinAppelantBll = new MedecinAppelantBLL();
            var addOrUpdateMedecinAppelantResult =
                medecinAppelantBll.AddOrUpdateMedecinAppelant(DossierDtoToAddOrUpdate.MedecinAppelantDto);
            if (addOrUpdateMedecinAppelantResult.Success)
            {
                var medecinAppelantAddedDto = addOrUpdateMedecinAppelantResult.Result;
                DossierDtoToAddOrUpdate.IdMedecinAppelant = medecinAppelantAddedDto.Id;
                DossierDtoToAddOrUpdate.MedecinAppelantDto = medecinAppelantAddedDto;
                DossierDtoToAddOrUpdate.IdUser = MainWindowViewModel.UserConnected.Id;

                var addDossierOperationResult =
                    _dossiersBll.AddOrUpdateDossier(DossierDtoToAddOrUpdate);
                var newDossierSaved = addDossierOperationResult.Result;
                if (addDossierOperationResult.Success)
                {
                    DossierDtoToAddOrUpdate.Id = newDossierSaved.Id;
                    DossierDtoToAddOrUpdate.MedecinAppelantDto.NumeroAnonymatGlobal = newDossierSaved.NumeroAnonymatGlobal;
                }
            }
        }

        public void ConsultMa(object obj)
        {
            MedecinAppelantEditFormViewModel.Instance.MedecinAppelantDtoToAddOrUpdate =
                DossierDtoToAddOrUpdate.MedecinAppelantDto;
            MedecinAppelantEditFormViewModel.Instance.IsFormEnabled = false;
            MedecinAppelantEditFormViewModel.Instance.IsSaveUserButtonVisible = Visibility.Collapsed;
            MedecinAppelantEditFormViewModel.Instance.IsValidateUserButtonVisible = Visibility.Collapsed;
            WindowHelper.ShowDialogWindow<MedecinAppelantEditForm>();
        }

        private void AddFormulaire(object obj)
        {
            WindowHelper.ShowDialogWindow<FormulaireChoiceListForm>();
        }

        #region Dossier Commands

        private ICommand _saveDossierCommand;

        public ICommand SaveDossierCommand
        {
            get
            {
                return _saveDossierCommand ?? (_saveDossierCommand =
                           new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = SaveDossier});
            }
        }

        private ICommand _consultMaCommand;

        public ICommand ConsultMaCommand
        {
            get
            {
                return _consultMaCommand ?? (_consultMaCommand =
                           new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = ConsultMa});
            }
        }

        #endregion

        #region Formulaires Commands

        private ICommand _editFormulaireCommand;

        public ICommand EditFormulaireCommand
        {
            get
            {
                return _editFormulaireCommand ?? (_editFormulaireCommand =
                           new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = EditFormulaire });
            }
        }

        private ICommand _deleteFormulaireCommand;

        public ICommand DeleteFormulaireCommand
        {
            get
            {
                return _deleteFormulaireCommand ?? (_deleteFormulaireCommand =
                           new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = DeleteFormulaire });
            }
        }

        private void DeleteFormulaire(object obj)
        {
            if (SelectedFormulaire == null)
            {
                return;
            }

            FormulaireBaseViewModel.Instance.Formulaire = SelectedFormulaire;
            FormulaireBaseViewModel.Instance.DeleteFormulaire();
        }

        private void EditFormulaire(object obj)
        {
            if (SelectedFormulaire == null)
            {
                return;
            }

            FormulaireBaseViewModel.Instance.Formulaire = SelectedFormulaire;
            FormulaireBaseViewModel.Instance.EditFormulaire();
            AsynchroneUpdateList();
        }

        private ICommand _addFormulaireCommand;
        private FormulairesBLL _formulairesBll;

        public ICommand AddFormulaireCommand
        {
            get
            {
                return _addFormulaireCommand ?? (_addFormulaireCommand =
                           new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = AddFormulaire});
            }
        }

        #endregion
    }
}
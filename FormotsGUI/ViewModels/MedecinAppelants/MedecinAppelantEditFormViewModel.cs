using System;
using System.Windows;
using System.Windows.Input;
using FormotsBLL.BLL;
using FormotsCommon.DTO;
using FormotsCommon.Utils;
using MahApps.Metro.Controls.Dialogs;

namespace FormotsGUI.ViewModels.MedecinAppelants
{
    public class MedecinAppelantEditFormViewModel : BaseViewModel
    {
        private readonly DialogService _dialogService;
        private readonly MedecinAppelantBLL _medecinAppelantsBll;
        private MedecinAppelantDto _medecinAppelantDtoToAddOrUpdate;
        private string _tiersAppelantExpanderTitle;

        private static MedecinAppelantEditFormViewModel _instance = new MedecinAppelantEditFormViewModel();

        public static MedecinAppelantEditFormViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
        
        public MedecinAppelantEditFormViewModel()
        {
            IsFormEnabled = true;
            _dialogService = new DialogService();
            _medecinAppelantsBll = new MedecinAppelantBLL();
        }

        public override string WindowTitle
        {
            get => GetWindowTitle(MedecinAppelantDtoToAddOrUpdate);
        }

        public MedecinAppelantDto MedecinAppelantDtoToAddOrUpdate
        {
            get => _medecinAppelantDtoToAddOrUpdate;
            set
            {
                if (Equals(value, _medecinAppelantDtoToAddOrUpdate))
                {
                    return;
                }

                _medecinAppelantDtoToAddOrUpdate = value;
                OnPropertyChanged("MedecinAppelantDtoToAddOrUpdate");
            }
        }

        private bool _isNewMedecinAppelant;
        public bool IsNewMedecinAppelant
        {
            get { return _isNewMedecinAppelant; }
            set
            {
                if (Equals(value, _isNewMedecinAppelant))
                {
                    return;
                }

                _isNewMedecinAppelant = value;
                OnPropertyChanged("IsNewMedecinAppelant");
            }
        }

        private bool _isFormEnabled;
        public bool IsFormEnabled
        {
            get => _isFormEnabled;
            set
            {
                if (Equals(value, _isFormEnabled))
                {
                    return;
                }

                _isFormEnabled = value;
                OnPropertyChanged("IsFormEnabled");
            }
        }

        private Visibility _isSaveUserButtonVisible;
        public Visibility IsSaveUserButtonVisible
        {
            get { return _isSaveUserButtonVisible; }
            set
            {
                if (Equals(value, _isSaveUserButtonVisible))
                {
                    return;
                }

                _isSaveUserButtonVisible = value;
                OnPropertyChanged("IsSaveUserButtonVisible");
            }
        }

        private Visibility _isValidateUserButtonVisible;
        public Visibility IsValidateUserButtonVisible
        {
            get { return _isValidateUserButtonVisible; }
            set
            {
                if (Equals(value, _isValidateUserButtonVisible))
                {
                    return;
                }

                _isValidateUserButtonVisible = value;
                OnPropertyChanged("IsValidateUserButtonVisible");
            }
        }

        public string TiersAppelantExpanderTitle
        {
            get => GetTiersAppelantExpanderTitle(MedecinAppelantDtoToAddOrUpdate);
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

        private static string GetWindowTitle(MedecinAppelantDto selectedMedecinAppelant)
        {
            if (selectedMedecinAppelant != null)
            {
                return $"Fiche MA - {selectedMedecinAppelant.Nom} {selectedMedecinAppelant.Prenom}";
            }
            return "Nouvelle fiche MA";
        }

        private static string GetTiersAppelantExpanderTitle(MedecinAppelantDto selectedMedecinAppelant)
        {
            if (selectedMedecinAppelant != null)
            {
                if (!string.IsNullOrEmpty(selectedMedecinAppelant.TiersNom))
                {
                    return $"Tiers-Appelant (TA) - {selectedMedecinAppelant.TiersNom}";
                }
                return "Tiers-Appelant (TA)";
            }
            return "Tiers-Appelant (TA)";
        }

        private void ValidateMedecinAppelant(object obj)
        {
            IsNewMedecinAppelant = true;
            OnClosingRequest();
        }

        public void CloseMedecinAppelant(object obj)
        {
            IsNewMedecinAppelant = false;
            OnClosingRequest();
        }

        private void SaveMedecinAppelant(object obj)
        {
            BaseSaveObject("le Médecin-Appelant", MedecinAppelantDtoToAddOrUpdate, IsNewMedecinAppelant, MedecinAppelantDtoToAddOrUpdate.Nom,
                MedecinAppelantDtoToAddOrUpdate => _medecinAppelantsBll.AddOrUpdateMedecinAppelant(MedecinAppelantDtoToAddOrUpdate),MedecinAppelantsListFormViewModel.Instance);
            OnClosingRequest();
        }

        #region Commands

        private ICommand _saveMedecinAppelantCommand;

        public ICommand SaveMedecinAppelantCommand
        {
            get
            {
                return _saveMedecinAppelantCommand ?? (_saveMedecinAppelantCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = SaveMedecinAppelant });
            }
        }

        private ICommand _validateMedecinAppelantCommand;

        public ICommand ValidateMedecinAppelantCommand
        {
            get
            {
                return _validateMedecinAppelantCommand ?? (_validateMedecinAppelantCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = ValidateMedecinAppelant });
            }
        }

        #endregion
    }
}
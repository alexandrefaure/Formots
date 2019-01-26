using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using FormotsBLL.BLL;
using FormotsCommon.DTO;
using FormotsCommon.Utils;
using FormotsGUI.Pages.Dossier;
using FormotsGUI.Pages.MedecinAppelant;
using FormotsGUI.ViewModels.Formulaires;
using FormotsGUI.ViewModels.MedecinAppelants;
using FormotsGUI.ViewModels.Users;
using FormotsGUI.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace FormotsGUI.ViewModels.Dossiers
{
    public class DossiersListFormViewModel : BaseViewModel
    {
        private const string DialogTitle = "Dossiers";

        private readonly object _dossiersListLock = new object();

        private readonly DialogService dialogService;

        private readonly DossiersBLL dossiersBLL = new DossiersBLL();

        //private ObservableCollection<DossierDto> _dossiersList;

        private DossierDto _selectedDossier;

        private static DossiersListFormViewModel _instance = new DossiersListFormViewModel();

        public static DossiersListFormViewModel Instance
        {
            get
            {
                return _instance;
            }
        }

        public DossiersListFormViewModel()
        {
            dialogService = new DialogService();
            DossiersList = new ObservableCollection<DossierDto>();
            //AsynchroneUpdateList();
        }

        private ObservableCollection<DossierDto> _dossiersList;
        public ObservableCollection<DossierDto> DossiersList
        {
            get { return _dossiersList; }
            set
            {
                if (_dossiersList == value)
                {
                    return;
                }

                _dossiersList = value;
                OnPropertyChanged("DossiersList");
                //Cette commande permet de mettre à jour la liste des dossiers d'un thread différent de celui de la GUI.
                BindingOperations.EnableCollectionSynchronization(DossiersList, _dossiersListLock);
            }
        }

        public DossierDto SelectedDossier
        {
            get => _selectedDossier;
            set
            {
                if (_selectedDossier == value)
                {
                    return;
                }
                _selectedDossier = value;
                OnPropertyChanged("SelectedDossier");
            }
        }

        private string _searchMedecinAppelantTextbox;
        public string SearchMedecinAppelantTextbox
        {
            get => _searchMedecinAppelantTextbox;
            set
            {
                if (_searchMedecinAppelantTextbox == value)
                {
                    return;
                }
                _searchMedecinAppelantTextbox = value;
                AsynchroneUpdateList();
                OnPropertyChanged("SearchMedecinAppelantTextbox");
            }
        }

        public void SortDossiersList()
        {
            SortDossiersListByMedecinAppelantNom();
        }

        private void SortDossiersListByMedecinAppelantNom()
        {
            if (!string.IsNullOrEmpty(SearchMedecinAppelantTextbox))
            {
                var sortedList = DossiersList.Where(x => x.MedecinAppelantDto.Nom.ToLower().Contains(SearchMedecinAppelantTextbox.ToLower()));
                DossiersList = new ObservableCollection<DossierDto>(sortedList);
            }
        }

        #region BackgroundWorker List

        protected override object DoWork(BackgroundWorker bgworker)
        {
            int i = 0;
            var asyncDossiersList = new ObservableCollection<DossierDto>();
            foreach (var dossier in dossiersBLL.GetDossiersList())
            {
                asyncDossiersList.Add(dossier);
                i++;
            }
            bgworker?.ReportProgress(i, asyncDossiersList);
            return asyncDossiersList;
        }

        protected override void ProgressChanged(ProgressChangedEventArgs e, object list)
        {
            ObservableCollection<DossierDto> partialresult;
            if (e == null && list != null)
            {
                partialresult = (ObservableCollection<DossierDto>)list;
            }
            else
            {
                partialresult = (ObservableCollection<DossierDto>)e.UserState;
            }

            DossiersList = new ObservableCollection<DossierDto>();
            foreach (var dossierDto in partialresult)
            {
                DossiersList.Add(dossierDto);
            }
        }

        protected override void RunWorkerCompleted()
        {
            SortDossiersList();
            OnPropertyChanged("DossiersList");
        }

        #endregion

        public override void Add(object sender)
        {
            var medecinAppelantsEditFormViewModelInstance = MedecinAppelantEditFormViewModel.Instance;
            medecinAppelantsEditFormViewModelInstance.MedecinAppelantDtoToAddOrUpdate = new MedecinAppelantDto();
            medecinAppelantsEditFormViewModelInstance.IsFormEnabled = true;
            medecinAppelantsEditFormViewModelInstance.IsSaveUserButtonVisible = Visibility.Collapsed;
            medecinAppelantsEditFormViewModelInstance.IsValidateUserButtonVisible = Visibility.Visible;
            WindowHelper.ShowDialogWindow<MedecinAppelantEditForm>();

            if (!medecinAppelantsEditFormViewModelInstance.IsNewMedecinAppelant)
            {
                return;
            }

            var newDossierDto = new DossierDto
            {
                MedecinAppelantDto = medecinAppelantsEditFormViewModelInstance.MedecinAppelantDtoToAddOrUpdate
            };
            DossierEditFormViewModel.Instance.DossierDtoToAddOrUpdate = newDossierDto;
            DossierEditFormViewModel.Instance.SaveDossierCommand.Execute(null);
            AsynchroneUpdateList();
            MedecinAppelantsListFormViewModel.Instance.AsynchroneUpdateList();
            DossierEditFormViewModel.Instance.AsynchroneUpdateList();
            WindowHelper.ShowDialogWindow<DossierEditForm>();
            DossiersListFormViewModel.Instance.AsynchroneUpdateList();
        }

        public override void Edit(object sender)
        {
            if (SelectedDossier == null)
            {
                return;
            }

            Action<string, string> exec = (s, s1) =>
            {
                DossierEditFormViewModel.Instance.DossierDtoToAddOrUpdate = SelectedDossier;
                DossierEditFormViewModel.Instance.SynchroneUpdateList();
            };
            var progressWindow = new ProgressWindow();
            progressWindow.SetTitle($"Ouverture du dossier {SelectedDossier.NumeroAnonymatGlobal} en cours");
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (object sender2, DoWorkEventArgs e) =>
            {
                exec.Invoke("path", "parameters");
            };
            backgroundWorker.RunWorkerCompleted += (object sender2, RunWorkerCompletedEventArgs e) =>
            {
                progressWindow.Close();
                WindowHelper.ShowDialogWindow<DossierEditForm>();
            };
            progressWindow.Show();
            backgroundWorker.RunWorkerAsync();
        }

        public override async void Delete(object sender)
        {
            BaseDeleteObject("le dossier", SelectedDossier, SelectedDossier.NumeroAnonymatGlobal,
                SelectedDossier => dossiersBLL.DeleteDossier(SelectedDossier));
        }
    }
}
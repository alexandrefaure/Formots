using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using FormotsBLL.BLL;
using FormotsCommon.DTO;
using FormotsCommon.Utils;
using FormotsGUI.Pages.MedecinAppelant;
using MahApps.Metro.Controls.Dialogs;

namespace FormotsGUI.ViewModels.MedecinAppelants
{
    public class MedecinAppelantsListFormViewModel : BaseViewModel
    {
        private const string DialogTitle = "Utilisateurs";

        private readonly object _usersListLock = new object();

        private MedecinAppelantDto _selectedMedecinAppelant;

        private ObservableCollection<MedecinAppelantDto> _medecinAppelantsList;

        private readonly DialogService _dialogService;

        private readonly MedecinAppelantBLL _medecineAppelantsBll = new MedecinAppelantBLL();

        private static MedecinAppelantsListFormViewModel _instance = new MedecinAppelantsListFormViewModel();

        public static MedecinAppelantsListFormViewModel Instance
        {
            get
            {
                return _instance;
            }
        }

        public MedecinAppelantsListFormViewModel()
        {
            _dialogService = new DialogService();
            MedecinAppelantsList = new ObservableCollection<MedecinAppelantDto>();
            //AsynchroneUpdateList();
        }

        public ObservableCollection<MedecinAppelantDto> MedecinAppelantsList
        {
            //get => _medecineAppelantsBll.GetFormulairesListByDossierId();
            get => _medecinAppelantsList;
            set
            {
                if (_medecinAppelantsList == value)
                {
                    return;
                }

                _medecinAppelantsList = value;
                OnPropertyChanged("MedecinAppelantsList");
                //Cette commande permet de mettre à jour la liste des users d'un thread différent de celui de la GUI.
                BindingOperations.EnableCollectionSynchronization(_medecinAppelantsList, _usersListLock);
            }
        }

        public MedecinAppelantDto SelectedMedecinAppelant
        {
            get => _selectedMedecinAppelant;
            set
            {
                if (_selectedMedecinAppelant == value)
                {
                    return;
                }
                _selectedMedecinAppelant = value;
                OnPropertyChanged("SelectedMedecinAppelant");
            }
        }

        private string _searchNameTextbox;
        public string SearchNameTextbox
        {
            get => _searchNameTextbox;
            set
            {

                if (_searchNameTextbox == value)
                {
                    return;
                }
                _searchNameTextbox = value;
                AsynchroneUpdateList();
                OnPropertyChanged("SearchLoginTextbox");
            }
        }

        public void SortMedecinAppelantsList()
        {
            SortMedecinAppelantsListByName();
        }

        private void SortMedecinAppelantsListByName()
        {
            if (!string.IsNullOrEmpty(SearchNameTextbox))
            {
                var sortedList = MedecinAppelantsList.Where(x => x.Nom.Contains(SearchNameTextbox));
                MedecinAppelantsList = new ObservableCollection<MedecinAppelantDto>(sortedList);
            }
        }

        #region BackgroundWorker List

        protected override object DoWork(BackgroundWorker bgworker)
        {
            int i = 0;
            var asyncMedecinAppelantList = new ObservableCollection<MedecinAppelantDto>();
            foreach (var medecinAppelant in _medecineAppelantsBll.GetMedecinAppelantsList())
            {
                asyncMedecinAppelantList.Add(medecinAppelant);
                i++;
            }
            bgworker?.ReportProgress(i, asyncMedecinAppelantList);
            return asyncMedecinAppelantList;
        }

        protected override void ProgressChanged(ProgressChangedEventArgs e, object list)
        {
            ObservableCollection<MedecinAppelantDto> partialresult;
            if (e == null && list != null)
            {
                partialresult = (ObservableCollection<MedecinAppelantDto>)list;
            }
            else
            {
                partialresult = (ObservableCollection<MedecinAppelantDto>)e.UserState;
            }

            MedecinAppelantsList = new ObservableCollection<MedecinAppelantDto>();
            foreach (var medecinAppelantDto in partialresult)
            {
                MedecinAppelantsList.Add(medecinAppelantDto);
            }
        }

        protected override void RunWorkerCompleted()
        {
            SortMedecinAppelantsList();
            OnPropertyChanged("MedecinAppelantsList");
        }

        #endregion

        public override void Edit(object sender)
        {
            if (SelectedMedecinAppelant == null)
            {
                return;
            }

            MedecinAppelantEditFormViewModel.Instance.MedecinAppelantDtoToAddOrUpdate = SelectedMedecinAppelant;
            MedecinAppelantEditFormViewModel.Instance.IsFormEnabled = true;
            MedecinAppelantEditFormViewModel.Instance.IsSaveUserButtonVisible = Visibility.Visible;
            MedecinAppelantEditFormViewModel.Instance.IsValidateUserButtonVisible = Visibility.Collapsed;

            WindowHelper.ShowDialogWindow<MedecinAppelantEditForm>();
            AsynchroneUpdateList();
        }

        public override async void Delete(object sender)
        {
            if (!string.IsNullOrEmpty(SelectedMedecinAppelant.NumeroAnonymatGlobal))
            {

                MessageBox.Show(
                    $"Impossible de supprimer le médecin {SelectedMedecinAppelant.Nom} car celui-ci est lié au dossier {SelectedMedecinAppelant.NumeroAnonymatGlobal}.",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            BaseDeleteObject("le médecin-appelant", SelectedMedecinAppelant, SelectedMedecinAppelant.Nom,
                SelectedMedecinAppelant => _medecineAppelantsBll.DeleteMedecinAppelant(SelectedMedecinAppelant));
        }
    }
}
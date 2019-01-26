using System.Windows.Input;
using FormotsBLL.BLL;
using FormotsCommon.Utils;
using FormotsGUI.ViewModels.Dossiers;
using FormotsGUI.ViewModels.Formulaires;
using FormotsGUI.ViewModels.MedecinAppelants;
using FormotsGUI.ViewModels.Statistiques;
using FormotsGUI.ViewModels.Users;

namespace FormotsGUI.ViewModels
{
    public class DashboardPageViewModel : BaseViewModel
    {
        private static DashboardPageViewModel _instance = new DashboardPageViewModel();

        public static DashboardPageViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
        private readonly DialogService _dialogService;

        public DashboardPageViewModel()
        {
            _dialogService = new DialogService();
        }


        private string _welcomeUserTitle;
        public string WelcomeUserTitle
        {
            get => _welcomeUserTitle;
            set
            {
                if (Equals(value, _welcomeUserTitle))
                {
                    return;
                }

                _welcomeUserTitle = value;
                OnPropertyChanged("WelcomeUserTitle");
            }
        }

        public int? DossiersCount
        {
            get => DossiersListFormViewModel.Instance.DossiersList.Count;
        }

        public int? FormulairesCount
        {
            get => FormulaireBaseViewModel.Instance.AllFormulairesDtoList.Count;
        }

        public int MedecinAppelantsCount
        {
            get => MedecinAppelantsListFormViewModel.Instance.MedecinAppelantsList.Count;
        }

        public int UtilisateursCount
        {
            get => UsersListFormViewModel.Instance.UsersList.Count;
        }

        private void GoToUsers(object obj)
        {
            if (MainWindowViewModel.Instance.OpenUsersPageCommand.CanExecute(null))
            {
                MainWindowViewModel.Instance.SelectedViewModel = UsersListFormViewModel.Instance;
            }
        }

        private void GoToMedecinAppelants(object obj)
        {
            if (MainWindowViewModel.Instance.OpenMedecinAppelantsPageCommand.CanExecute(null))
            {
                MainWindowViewModel.Instance.SelectedViewModel = MedecinAppelantsListFormViewModel.Instance;
            }
        }

        private void GoToDossiers(object obj)
        {
            if (MainWindowViewModel.Instance.OpenDossiersPageCommand.CanExecute(null))
            {
                MainWindowViewModel.Instance.SelectedViewModel = DossiersListFormViewModel.Instance;
            }
        }

        private void GoToCharts(object obj)
        {
            if (MainWindowViewModel.Instance.OpenStatisticsPageCommand.CanExecute(null))
            {
                var chartsListFormViewModel = ChartsListFormViewModel.Instance;
                MainWindowViewModel.OpenAsynchroneStatisticsPage(chartsListFormViewModel);
                MainWindowViewModel.Instance.SelectedViewModel = ChartsListFormViewModel.Instance;
            }
        }

        #region Commands

        private ICommand _goToUsersCommand;

        public ICommand GoToUsersCommand
        {
            get
            {
                return _goToUsersCommand ?? (_goToUsersCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = GoToUsers });
            }
        }

        private ICommand _goToMedecinAppelantsCommand;

        public ICommand GoToMedecinAppelantsCommand
        {
            get
            {
                return _goToMedecinAppelantsCommand ?? (_goToMedecinAppelantsCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = GoToMedecinAppelants });
            }
        }

        private ICommand _goToDossiersCommand;

        public ICommand GoToDossiersCommand
        {
            get
            {
                return _goToDossiersCommand ?? (_goToDossiersCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = GoToDossiers });
            }
        }

        private ICommand _goToChartsCommand;

        public ICommand GoToChartsCommand
        {
            get
            {
                return _goToChartsCommand ?? (_goToChartsCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = GoToCharts });
            }
        }
        
        #endregion
    }
}

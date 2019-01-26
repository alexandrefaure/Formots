using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FormotsBLL.BLL;
using FormotsCommon.DTO;
using FormotsGUI.Windows;
using FormotsCommon.Utils;
using FormotsGUI.ViewModels.Dossiers;
using FormotsGUI.ViewModels.Formulaires;
using FormotsGUI.ViewModels.MedecinAppelants;
using FormotsGUI.ViewModels.Statistiques;
using FormotsGUI.ViewModels.Users;

namespace FormotsGUI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static MainWindowViewModel _instance = new MainWindowViewModel();

        public static MainWindowViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
        private readonly DialogService _dialogService;
        public MainWindowViewModel()
        {
            _dialogService = new DialogService();
            InitializeApplicationViewModels();
        }

        private void InitializeApplicationViewModels()
        {
            formulaireBaseViewModel = FormulaireBaseViewModel.Instance;
            usersListFormViewModel = UsersListFormViewModel.Instance;
            medecinAppelantsListFormViewModel = MedecinAppelantsListFormViewModel.Instance;
            dossiersListFormViewModel = DossiersListFormViewModel.Instance;
            dashboardPageViewModel = DashboardPageViewModel.Instance;
        }

        private UsersListFormViewModel usersListFormViewModel;
        private MedecinAppelantsListFormViewModel medecinAppelantsListFormViewModel;
        private DossiersListFormViewModel dossiersListFormViewModel;
        private DashboardPageViewModel dashboardPageViewModel;
        private FormulaireBaseViewModel formulaireBaseViewModel;
        private ChartsListFormViewModel chartsListFormViewModel;

        #region Commands

        private ICommand _openUsersPageCommand;

        public ICommand OpenUsersPageCommand
        {
            get { return _openUsersPageCommand ?? (_openUsersPageCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = OpenUsersPage }); }
        }

        private ICommand _openDashboardPageCommand;

        public ICommand OpenDashboardPageCommand
        {
            get { return _openDashboardPageCommand ?? (_openDashboardPageCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = OpenDashboardPage }); }
        }

        private ICommand _openFormsPageCommand;

        public ICommand OpenFormsPageCommand
        {
            get { return _openFormsPageCommand ?? (_openFormsPageCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = OpenFormsPage }); }
        }

        private ICommand _openMedecinAppelantsPageCommand;

        public ICommand OpenMedecinAppelantsPageCommand
        {
            get { return _openMedecinAppelantsPageCommand ?? (_openMedecinAppelantsPageCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = OpenMedecinAppelantsPage }); }
        }

        private ICommand _openDossiersPageCommand;

        public ICommand OpenDossiersPageCommand
        {
            get { return _openDossiersPageCommand ?? (_openDossiersPageCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = OpenDossiersPage }); }
        }

        private ICommand _openStatisticsPageCommand;

        public ICommand OpenStatisticsPageCommand
        {
            get { return _openStatisticsPageCommand ?? (_openStatisticsPageCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = OpenStatisticsPage }); }
        }

        #endregion

        public virtual void OpenFormsPage(object sender)
        {
            var newWindow = new MainWindow();
            newWindow.Show();
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            var askConfirmQuitResult =
                MessageBox.Show("Êtes-vous sûr de vouloir quitter l'application ?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (askConfirmQuitResult != MessageBoxResult.Yes)
            {
                if (e != null)
                {
                    e.Cancel = true;
                }
            }
        }

        public virtual void OpenUsersPage(object sender)
        {
            //var usersListFormViewModel = UsersListFormViewModel.Instance;
            usersListFormViewModel.AsynchroneUpdateList();
            SelectedViewModel = usersListFormViewModel;
        }

        public virtual void OpenMedecinAppelantsPage(object sender)
        {
            //var medecinAppelantsListFormViewModel = MedecinAppelantsListFormViewModel.Instance;
            medecinAppelantsListFormViewModel.AsynchroneUpdateList();
            SelectedViewModel = medecinAppelantsListFormViewModel;
        }

        public virtual void OpenDossiersPage(object sender)
        {
            //var dossiersListFormViewModel = DossiersListFormViewModel.Instance;
            dossiersListFormViewModel.AsynchroneUpdateList();
            SelectedViewModel = DossiersListFormViewModel.Instance;
        }

        public virtual void OpenStatisticsPage(object sender)
        {
            var chartsListFormViewModel = ChartsListFormViewModel.Instance;
            SelectedViewModel = chartsListFormViewModel;
            OpenAsynchroneStatisticsPage(chartsListFormViewModel);
        }

        public static void OpenAsynchroneStatisticsPage(ChartsListFormViewModel chartsListFormViewModel)
        {
            Action<string, string> exec = (s, s1) =>
            {
                chartsListFormViewModel.ChartsDtoList = StatistiquesBLL.Current.GetChartsDtoList();
            };

            var progressWindow = new ProgressWindow();
            progressWindow.SetTitle("Chargement en cours");
            var backgroundWorker = new BackgroundWorker();

            // set the worker to call your long-running method
            backgroundWorker.DoWork += (object sender2, DoWorkEventArgs e) => { exec.Invoke("path", "parameters"); };

            // set the worker to close your progress form when it's completed
            backgroundWorker.RunWorkerCompleted += (object sender2, RunWorkerCompletedEventArgs e) =>
            {
                progressWindow.Close();
            };
            progressWindow.Show();
            backgroundWorker.RunWorkerAsync();
        }

        protected virtual void OpenDashboardPage(object sender)
        {
            //var dashboardPageViewModel = DashboardPageViewModel.Instance;
            SelectedViewModel = dashboardPageViewModel;
        }

        private object selectedViewModel;

        public object SelectedViewModel

        {
            get
            {
                return selectedViewModel;
            }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        private static UserDto _userConnected;
        public static UserDto UserConnected
        {
            get => _userConnected;
            set
            {
                if (Equals(value, _userConnected))
                {
                    return;
                }

                _userConnected = value;
                //OnPropertyChanged("UserConnected");
            }
        }
    }
}

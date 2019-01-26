using System.Windows.Input;
using FormotsCommon.Utils;
using MOTS.ViewModels;

namespace FormotsGUI.ViewModels.Statistiques
{
    public class ChartWindowViewModel : BaseViewModel
    {
        private static ChartWindowViewModel _instance = new ChartWindowViewModel();

        public static ChartWindowViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
        private readonly DialogService _dialogService;
        public ChartWindowViewModel()
        {
            _dialogService = new DialogService();
            //InitializeApplicationViewModels();
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

        private ICommand _exportCommand;

        public ICommand ExportCommand
        {
            get
            {
                return _exportCommand ?? (_exportCommand =
                           new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = Export });
            }
        }

        private void Export(object obj)
        {
            var selectedViewModel = SelectedViewModel as BaseChartViewModel;
            if (selectedViewModel != null)
            {
                selectedViewModel.ExportToExcel();
            }
        }
    }
}

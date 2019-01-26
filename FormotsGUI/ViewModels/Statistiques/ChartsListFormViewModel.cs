using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using FormotsCommon.DTO;
using FormotsCommon.Helper;
using FormotsCommon.Utils;
using FormotsGUI.Windows;

namespace FormotsGUI.ViewModels.Statistiques
{
    public class ChartsListFormViewModel : BaseViewModel
    {
        private static ChartsListFormViewModel _instance = new ChartsListFormViewModel();
        public static ChartsListFormViewModel Instance
        {
            get
            {
                return _instance;
            }
        }

        private readonly object _chartsListLock = new object();

        private ObservableCollection<ChartDto> _chartsDtoList;

        private ICommand _generateChartCommand;

        public ChartsListFormViewModel()
        {
            ChartsDtoList = new ObservableCollection<ChartDto>();
        }

        public ObservableCollection<ChartDto> ChartsDtoList
        {
            get => _chartsDtoList;
            set
            {
                if (_chartsDtoList == value)
                {
                    return;
                }

                _chartsDtoList = value;
                OnPropertyChanged("ChartsDtoList");
                //Cette commande permet de mettre à jour la liste des dossiers d'un thread différent de celui de la GUI.
                BindingOperations.EnableCollectionSynchronization(ChartsDtoList, _chartsListLock);
            }
        }

        private ChartDto _selectedChart;
        public ChartDto SelectedChart
        {
            get => _selectedChart;
            set
            {
                if (_selectedChart == value)
                {
                    return;
                }

                _selectedChart = value;
                OnPropertyChanged("SelectedChart");
            }
        }

        public ICommand GenerateChartCommand
        {
            get
            {
                return _generateChartCommand ?? (_generateChartCommand =
                           new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = GenerateChart});
            }
        }

        private void GenerateChart(object obj)
        {
            if (SelectedChart == null)
            {
                return;
            }

            var chartWindowViewModel = ChartWindowViewModel.Instance;
            if (SelectedChart.Type == ChartsHelper.ChartType.Histogram)
            {

                var chartHistogramFormViewModel = ChartHistogramFormViewModel.Instance;
                chartWindowViewModel.SelectedViewModel = chartHistogramFormViewModel;

                chartHistogramFormViewModel.GenerateHistogram(SelectedChart);
                WindowHelper.ShowDialogWindow<ChartWindow>();
            }
            else if (SelectedChart.Type == ChartsHelper.ChartType.Pie)
            {

                var chartPieFormViewModel = ChartPieFormViewModel.Instance;
                chartWindowViewModel.SelectedViewModel = chartPieFormViewModel;

                chartPieFormViewModel.GenerateHistogram(SelectedChart);
                WindowHelper.ShowDialogWindow<ChartWindow>();
            }
        }
    }
}
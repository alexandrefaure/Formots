using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FormotsBLL.BLL;
using FormotsCommon.DTO;
using FormotsGUI.ViewModels.Dossiers;
using FormotsGUI.ViewModels.Formulaires;
using FormotsGUI.ViewModels.MedecinAppelants;
using FormotsGUI.ViewModels.Users;
using FormotsGUI.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using MOTS.ViewModels;

namespace FormotsGUI.ViewModels.Statistiques
{
    public class ChartPieFormViewModel : BaseChartViewModel
    {
        private static ChartPieFormViewModel _instance = new ChartPieFormViewModel();

        public static ChartPieFormViewModel Instance
        {
            get
            {
                return _instance;
            }
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        public ChartPieFormViewModel()
        {

        }

        public void GenerateHistogram(ChartDto chartDto)
        {
            if (chartDto == null)
            {
                return;
            }

            currentChartDto = chartDto;

            Action<string, string> exec = (s, s1) =>
            {
                ChartsListFormViewModel.Instance.ChartsDtoList = StatistiquesBLL.Current.GetChartsDtoList(); //Permet de remettre à jour le graphique
            };

            var progressWindow = new ProgressWindow();
            progressWindow.SetTitle("Chargement en cours");
            var backgroundWorker = new BackgroundWorker();

            // set the worker to call your long-running method
            backgroundWorker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                exec.Invoke("path", "parameters");
            };

            // set the worker to close your progress form when it's completed
            backgroundWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                progressWindow.Close();
            };
            progressWindow.Show();
            backgroundWorker.RunWorkerAsync();

            SeriesCollection = chartDto.SeriesCollection;

            ChartTitle = chartDto.ChartTitle;
            TitleXAxis = chartDto.TitleXAxis;
            TitleYAxis = chartDto.TitleYAxis;
            Labels = chartDto.Labels.ToArray();
            Formatter = value => value.ToString("N");

            SeriesCollection = new SeriesCollection();
            foreach (var chartDtoValue in chartDto.Values)
            {

                    SeriesCollection.Add(new PieSeries
                    {
                        Title = chartDtoValue.SerieTitle,
                        Values = new ChartValues<int>{
                            chartDtoValue.Count},
                        DataLabels = true,
                        LabelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
            });
            }
        }

        public void SetLabels(List<string> labelsList)
        {
            Labels = labelsList.ToArray();
        }

        public void GenerateFakeHistogramTest()
        {
            //PointLabel = chartPoint =>
            //    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            //var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            ////clear selected slice.
            //foreach (PieSeries series in chart.Series)
            //    series.PushOut = 0;

            //var selectedSeries = (PieSeries)chartpoint.SeriesView;
            //selectedSeries.PushOut = 8;
        }

        private string _chartTitle;
        public string ChartTitle
        {
            get => _chartTitle;
            set
            {
                if (_chartTitle == value)
                {
                    return;
                }
                _chartTitle = value;
                OnPropertyChanged("ChartTitle");
            }
        }

        private string _titleXAxis;
        public string TitleXAxis
        {
            get => _titleXAxis;
            set
            {
                if (_titleXAxis == value)
                {
                    return;
                }
                _titleXAxis = value;
                OnPropertyChanged("TitleXAxis");
            }
        }

        private string _titleYAxis;
        public string TitleYAxis
        {
            get => _titleYAxis;
            set
            {
                if (_titleYAxis == value)
                {
                    return;
                }
                _titleYAxis = value;
                OnPropertyChanged("TitleYAxis");
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
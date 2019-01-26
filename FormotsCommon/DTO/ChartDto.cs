using System.Collections.Generic;
using System.ComponentModel;
using FormotsCommon.FormValidation;
using FormotsCommon.Helper;
using LiveCharts;
using LiveCharts.Wpf;

namespace FormotsCommon.DTO
{
    public class ChartDto : DataErrorInfo, INotifyPropertyChanged
    {
        private ChartsHelper.ChartType _type;
        public ChartsHelper.ChartType Type
        {
            get { return _type; }
            set
            {
                if (Equals(value, _type))
                {
                    return;
                }

                _type = value;
                OnPropertyChanged("Type");
            }
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

        private List<string> _labels;
        public List<string> Labels
        {
            get => _labels;
            set
            {
                if (_labels == value)
                {
                    return;
                }
                _labels = value;
                OnPropertyChanged("Labels");
            }
        }

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set
            {
                if (_seriesCollection == value)
                {
                    return;
                }
                _seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        private List<SeriesHandler> _values;
        public List<SeriesHandler> Values
        {
            get => _values;
            set
            {
                if (_values == value)
                {
                    return;
                }
                _values = value;
                OnPropertyChanged("Values");
            }
        }

        public virtual void GenerateChart()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

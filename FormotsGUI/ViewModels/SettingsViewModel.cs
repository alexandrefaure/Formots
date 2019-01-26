using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using MahApps.Metro;
using FormotsCommon.Utils;

namespace FormotsGUI.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private List<AccentColorMenuData> _themeColors;
        public List<AccentColorMenuData> ThemeColors
        {
            get { return _themeColors; }
            set
            {
                if (_themeColors == value) return;
                _themeColors = value;
                OnPropertyChanged("ThemeColors");
            }
        }

        public SettingsViewModel()
        {
            // create accent color menu items for the demo
            ThemeColors = ThemeManager.Accents
                .Select(a => new AccentColorMenuData { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                .ToList();
        }

        private AccentColorMenuData _selectedThemeColor;
        public AccentColorMenuData SelectedThemeColor
        {
            get { return _selectedThemeColor; }
            set
            {
                if (_selectedThemeColor == value) return;
                _selectedThemeColor = value;
                ChangeColorApplicationTheme();
                OnPropertyChanged("SelectedThemeColor");
            }
        }

        protected virtual void ChangeColorApplicationTheme()
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(SelectedThemeColor.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

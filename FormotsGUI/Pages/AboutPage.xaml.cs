using System.Windows.Controls;
using FormotsGUI.ViewModels;
using MOTS.ViewModels;

namespace FormotsGUI.Pages
{
    /// <summary>
    /// Logique d'interaction pour SettingsPage.xaml
    /// </summary>
    public partial class AboutPage : UserControl
    {
        public AboutPage()
        {
            InitializeComponent();
            //var settingsViewModel = new SettingsViewModel();
            //DataContext = settingsViewModel;
            DataContext = new AboutPageViewModel();
        }
    }
}

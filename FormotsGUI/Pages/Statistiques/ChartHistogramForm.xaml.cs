using System.Windows.Controls;
using FormotsGUI.ViewModels.Dossiers;
using FormotsGUI.ViewModels.Statistiques;

namespace FormotsGUI.Pages.Statistiques
{
    /// <summary>
    /// Logique d'interaction pour UsersPage.xaml
    /// </summary>
    public partial class ChartHistogramForm : UserControl
    {
        public ChartHistogramForm()
        {
            InitializeComponent();
            DataContext = ChartHistogramFormViewModel.Instance;
        }
    }
}

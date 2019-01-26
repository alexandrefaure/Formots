using System.Windows.Controls;
using FormotsGUI.ViewModels.Statistiques;

namespace FormotsGUI.Pages.Statistiques
{
    /// <summary>
    /// Logique d'interaction pour UsersPage.xaml
    /// </summary>
    public partial class ChartPieForm : UserControl
    {
        public ChartPieForm()
        {
            InitializeComponent();
            DataContext = ChartPieFormViewModel.Instance;
        }
    }
}

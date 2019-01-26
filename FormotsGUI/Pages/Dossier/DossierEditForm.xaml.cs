using FormotsGUI.ViewModels.Dossiers;
using MahApps.Metro.Controls;

namespace FormotsGUI.Pages.Dossier
{
    /// <summary>
    /// Logique d'interaction pour MedecinAppelantAddWindow.xaml
    /// </summary>
    public partial class DossierEditForm : MetroWindow
    {
        public DossierEditForm()
        {
            InitializeComponent();
            var viewModel = DossierEditFormViewModel.Instance;
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => Close();
        }
    }
}

using FormotsGUI.ViewModels.MedecinAppelants;
using MahApps.Metro.Controls;

namespace FormotsGUI.Pages.MedecinAppelant
{
    /// <summary>
    /// Logique d'interaction pour MedecinAppelantAddWindow.xaml
    /// </summary>
    public partial class MedecinAppelantEditForm : MetroWindow
    {
        public MedecinAppelantEditForm()
        {
            InitializeComponent();
            var viewModel = MedecinAppelantEditFormViewModel.Instance;
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => Close();
        }
    }
}

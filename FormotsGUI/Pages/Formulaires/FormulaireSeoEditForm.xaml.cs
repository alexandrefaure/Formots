using FormotsGUI.ViewModels.Formulaires;
using MahApps.Metro.Controls;

namespace FormotsGUI.Pages.Formulaires
{
    /// <summary>
    /// Logique d'interaction pour MedecinAppelantAddWindow.xaml
    /// </summary>
    public partial class FormulaireSeoEditForm : MetroWindow
    {
        public FormulaireSeoEditForm()
        {
            InitializeComponent();
            var viewModel = FormulaireSeoEditFormViewModel.Instance;
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => Close();
        }
    }
}

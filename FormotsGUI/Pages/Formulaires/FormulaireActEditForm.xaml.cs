using FormotsGUI.ViewModels.Formulaires;
using MahApps.Metro.Controls;

namespace FormotsGUI.Pages.Formulaires
{
    /// <summary>
    /// Logique d'interaction pour MedecinAppelantAddWindow.xaml
    /// </summary>
    public partial class FormulaireActEditForm : MetroWindow
    {
        public FormulaireActEditForm()
        {
            InitializeComponent();
            var viewModel = FormulaireActEditFormViewModel.Instance;
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => Close();
        }
    }
}

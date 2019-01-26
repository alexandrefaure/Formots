using FormotsGUI.ViewModels.Formulaires;
using MahApps.Metro.Controls;

namespace FormotsGUI.Pages.Formulaires
{
    /// <summary>
    /// Logique d'interaction pour MedecinAppelantAddWindow.xaml
    /// </summary>
    public partial class FormulaireCdaEditForm : MetroWindow
    {
        public FormulaireCdaEditForm()
        {
            InitializeComponent();
            var viewModel = FormulaireCdaEditFormViewModel.Instance;
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => Close();
        }
    }
}

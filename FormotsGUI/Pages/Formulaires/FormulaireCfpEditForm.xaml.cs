using FormotsGUI.ViewModels.Formulaires;
using MahApps.Metro.Controls;

namespace FormotsGUI.Pages.Formulaires
{
    /// <summary>
    /// Logique d'interaction pour MedecinAppelantAddWindow.xaml
    /// </summary>
    public partial class FormulaireCfpEditForm : MetroWindow
    {
        public FormulaireCfpEditForm()
        {
            InitializeComponent();
            var viewModel = FormulaireCfpEditFormViewModel.Instance;
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => Close();
        }
    }
}

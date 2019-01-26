using System.Windows.Controls;
using FormotsGUI.ViewModels;
using FormotsGUI.ViewModels.Dossiers;

namespace FormotsGUI.Pages.Dossier
{
    /// <summary>
    /// Logique d'interaction pour UsersPage.xaml
    /// </summary>
    public partial class DossiersListForm : UserControl
    {
        public DossiersListForm()
        {
            InitializeComponent();
            DataContext = DossiersListFormViewModel.Instance;
        }
    }
}

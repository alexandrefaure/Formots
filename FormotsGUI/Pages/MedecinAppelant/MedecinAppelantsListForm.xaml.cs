using System.Windows.Controls;
using FormotsGUI.ViewModels;
using FormotsGUI.ViewModels.MedecinAppelants;

namespace FormotsGUI.Pages.MedecinAppelant
{
    /// <summary>
    /// Logique d'interaction pour UsersPage.xaml
    /// </summary>
    public partial class MedecinAppelantsListForm : UserControl
    {
        public MedecinAppelantsListForm()
        {
            InitializeComponent();
            DataContext = MedecinAppelantsListFormViewModel.Instance;
        }
    }
}

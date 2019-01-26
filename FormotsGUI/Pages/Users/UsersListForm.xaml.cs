using System.Windows.Controls;
using FormotsGUI.ViewModels.Users;

namespace FormotsGUI.Pages.Users
{
    /// <summary>
    /// Logique d'interaction pour UsersPage.xaml
    /// </summary>
    public partial class UsersListForm : UserControl
    {
        public UsersListForm()
        {
            InitializeComponent();
            DataContext = UsersListFormViewModel.Instance;
        }
    }
}

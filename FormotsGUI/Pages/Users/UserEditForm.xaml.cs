using FormotsGUI.ViewModels.Users;
using MahApps.Metro.Controls;

namespace FormotsGUI.Pages.Users
{
    /// <summary>
    /// Logique d'interaction pour UserAddControl.xaml
    /// </summary>
    public partial class UserEditForm : MetroWindow
    {
        public UserEditForm()
        {
            InitializeComponent();
            var viewModel = UserEditFormViewModel.Instance;
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => Close();
        }
    }
}

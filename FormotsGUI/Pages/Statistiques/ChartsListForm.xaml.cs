using System.Windows.Input;
using FormotsGUI.ViewModels.Dossiers;
using FormotsGUI.ViewModels.Statistiques;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace FormotsGUI.Pages.Statistiques
{
    /// <summary>
    /// Logique d'interaction pour UsersPage.xaml
    /// </summary>
    public partial class ChartsListForm : UserControl
    {
        public ChartsListForm()
        {
            InitializeComponent();
            DataContext = ChartsListFormViewModel.Instance;
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ChartsListFormViewModel)DataContext).GenerateChartCommand.CanExecute(null))
            {
                ((ChartsListFormViewModel)DataContext).GenerateChartCommand.Execute(null);
            }
        }
    }
}

using System.Windows.Controls;
using FormotsGUI.ViewModels;

namespace FormotsGUI.Pages
{
    /// <summary>
    /// Logique d'interaction pour DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : UserControl
    {
        public DashboardPage()
        {
            InitializeComponent();
            DataContext = DashboardPageViewModel.Instance;
        }
    }
}

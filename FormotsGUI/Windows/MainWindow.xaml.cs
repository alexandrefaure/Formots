using System.Windows.Input;
using FormotsGUI.ViewModels;
using MahApps.Metro.Controls;

namespace FormotsGUI.Windows
{
    /// <summary>
    ///     Logique d'interaction pour Window.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = MainWindowViewModel.Instance;
            DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
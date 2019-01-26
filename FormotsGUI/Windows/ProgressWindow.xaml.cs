using System.Windows;
using MahApps.Metro.Controls;

namespace FormotsGUI.Windows
{
    /// <summary>
    /// Logique d'interaction pour ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : MetroWindow
    {
        public ProgressWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }
    }
}

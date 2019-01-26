using System.Windows;
using FormotsGUI.ViewModels;
using MahApps.Metro.Controls;

namespace FormotsGUI.Windows
{
    /// <summary>
    ///     Logique d'interaction pour FirstLoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginWindowViewModel();
        }

        private void PasswordTextbox_TextChanged(object sender, RoutedEventArgs e)
        {
            var loginViewModel = (LoginWindowViewModel) DataContext;
            if (loginViewModel.IsFirstLogin)
            {
                if (!PasswordTextbox.Password.Equals(string.Empty) &&
                    ConfirmPasswordTextbox.Password.Equals(PasswordTextbox.Password))
                {
                    SaveButton.IsEnabled = true;
                    loginViewModel.LoginUser.Password = PasswordTextbox.Password;
                }
                else
                {
                    SaveButton.IsEnabled = false;
                }
            }
            else
            {
                if (!PasswordTextbox.Password.Equals(string.Empty))
                {
                    SaveButton.IsEnabled = true;
                    loginViewModel.LoginUser.Password = PasswordTextbox.Password;
                }
                else
                {
                    SaveButton.IsEnabled = false;
                }
            }
        }
    }
}
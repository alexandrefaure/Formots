using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FormotsBLL;
using FormotsBLL.BLL;
using FormotsCommon.DTO;
using FormotsCommon.FormValidation;
using FormotsCommon.Utils;
using MahApps.Metro.Controls.Dialogs;

namespace FormotsGUI.ViewModels
{
    public class LoginWindowViewModel : DataErrorInfo, INotifyPropertyChanged
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        public string ConnectionTitleFormText { get; set; }
        public string SaveConnectButtonText { get; set; }
        public bool IsFirstLogin { get; set; }

        public LoginWindowViewModel()
        {
            _dialogCoordinator = new DialogCoordinator();
            LoginUser = new UserDto();
            var usersBll = new UsersBLL();
            var usersCount = usersBll.GetUsersList().Count;
            if (usersCount == 0)
            {
                IsFirstLogin = true;
                ConnectionTitleFormText = "Bienvenue dans MOTS";
                SaveConnectButtonText = "Enregistrer";
            }
            else
            {
                IsFirstLogin = false;
                ConnectionTitleFormText = "Connexion à MOTS";
                SaveConnectButtonText = "Connecter";
            }
        }

        private UserDto _loginUser;
        public UserDto LoginUser
        {
            get => _loginUser;
            set
            {
                if (Equals(value, _loginUser))
                {
                    return;
                }

                _loginUser = value;
                OnPropertyChanged("LoginUser");
            }
        }

        #region Commands

        private ICommand _saveConnectCommand;

        public ICommand SaveConnectCommand
        {
            get
            {
                return _saveConnectCommand ?? (_saveConnectCommand =
                           new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = SaveOrConnectUser });
            }
        }

        private void SaveOrConnectUser(object obj)
        {
            if (IsFirstLogin)
            {
                SaveNewUser(obj);
            }
            else
            {
                ConnectUser(obj);
            }
        }

        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand =
                           new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = Cancel});
            }
        }

        #endregion


        private void Cancel(object obj)
        {
            var mainWindow = (Window) obj;
            mainWindow.DialogResult = false;
        }


        public void ConnectUser(object obj)
        {
            var mainWindow = (Window)obj;
            var usersBll = new UsersBLL();
            var connectUserOperationResult = usersBll.ConnectUser(LoginUser);
            if (connectUserOperationResult.Success)
            {
                LoginUser = connectUserOperationResult.Result;
                MainWindowViewModel.UserConnected = LoginUser;
                mainWindow.DialogResult = true;
            }
            else
            {
                ShowMessage(
                    connectUserOperationResult.NonSuccessMessage);
            }
        }

        private void SaveNewUser(object obj)
        {
            var mainWindow = (Window) obj;
            var usersBll = new UsersBLL();
            var addUserOperationResult = usersBll.AddOrUpdateUser(LoginUser);
            if (addUserOperationResult.Success)
            {
                MainWindowViewModel.UserConnected = addUserOperationResult.Result;
                ShowMessage($"L'utilisateur {LoginUser.Login} a bien été créé.");

                mainWindow.DialogResult = true;
            }
            else
            {
                ShowMessage(
                    $"L'utilisateur {LoginUser.Login} n'a pas pu être créé. Veuillez contacter votre administrateur.");
                mainWindow.DialogResult = false;
            }
        }

        public void ShowMessage(string message)
        {
            _dialogCoordinator.ShowMessageAsync(this, "Titre", message, MessageDialogStyle.Affirmative,
                MainDialogManager.GetOuiNonAnnulerDialogSettings());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
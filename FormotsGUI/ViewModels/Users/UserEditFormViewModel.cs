using System.ComponentModel;
using System.Windows.Input;
using FormotsBLL.BLL;
using FormotsCommon.DTO;
using FormotsCommon.Utils;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace FormotsGUI.ViewModels.Users
{
    public class UserEditFormViewModel : BaseViewModel
    {
        private readonly DialogService dialogService;

        private bool _isNewUser;
        public bool IsNewUser
        {
            get => _isNewUser;
            set
            {
                if (Equals(value, _isNewUser))
                {
                    return;
                }

                _isNewUser = value;
                OnPropertyChanged("IsNewUser");
            }
        }

        private UserDto _userDtoToAddOrUpdate;

        private static UserEditFormViewModel _instance = new UserEditFormViewModel();

        public static UserEditFormViewModel Instance
        {
            get
            {
                return _instance;
            }
        }

        public UserEditFormViewModel()
        {
            dialogService = new DialogService();
            _userBll = new UsersBLL();
        }

        public UserDto UserDtoToAddOrUpdate
        {
            get => _userDtoToAddOrUpdate;
            set
            {
                if (Equals(value, _userDtoToAddOrUpdate))
                {
                    return;
                }

                _userDtoToAddOrUpdate = value;
                OnPropertyChanged("UserDtoToAddOrUpdate");
            }
        }

        public override string WindowTitle
        {
            get => GetWindowTitle(UserDtoToAddOrUpdate);
        }

        private static string GetWindowTitle(UserDto selectedUser)
        {
            if (selectedUser != null)
            {
                return $"Fiche utilisateur - {selectedUser.LastName} {selectedUser.FirstName}";
            }
            return "Nouvelle ficher utilisateur";
        }

        private void SaveUser(object obj)
        {
            BaseSaveObject("l'utilisateur", UserDtoToAddOrUpdate, IsNewUser, UserDtoToAddOrUpdate.Login,
                UserDtoToAddOrUpdate => _userBll.AddOrUpdateUser(UserDtoToAddOrUpdate), UsersListFormViewModel.Instance);
            OnClosingRequest();
        }

        #region Commands

        private ICommand _saveUserCommand;
        private UsersBLL _userBll;

        public ICommand SaveUserCommand
        {
            get
            {
                return _saveUserCommand ?? (_saveUserCommand =
                           new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = SaveUser});
            }
        }

        #endregion
    }
}
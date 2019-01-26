using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using FormotsBLL.BLL;
using FormotsCommon.DTO;
using FormotsCommon.Utils;
using FormotsGUI.Pages.Users;

namespace FormotsGUI.ViewModels.Users
{
    public class UsersListFormViewModel : BaseViewModel
    {
        public override string DialogTitle => "Utilisateurs";

        private readonly object _usersListLock = new object();

        private UserDto _selectedUser;

        private ObservableCollection<UserDto> _usersList;

        private readonly DialogService _dialogService;

        private readonly UsersBLL _usersBll = new UsersBLL();

        private static UsersListFormViewModel _instance = new UsersListFormViewModel();

        public static UsersListFormViewModel Instance
        {
            get
            {
                return _instance;
            }
        }

        public UsersListFormViewModel()
        {
            _dialogService = new DialogService();
            UsersList = new ObservableCollection<UserDto>();
            //AsynchroneUpdateList();
        }

        public ObservableCollection<UserDto> UsersList
        {
            get => _usersList;
            set
            {
                if (_usersList == value)
                {
                    return;
                }

                _usersList = value;

                OnPropertyChanged("UsersList");
                //Cette commande permet de mettre à jour la liste des users d'un thread différent de celui de la GUI.
                BindingOperations.EnableCollectionSynchronization(_usersList, _usersListLock);
            }
        }

        public UserDto SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser == value)
                {
                    return;
                }
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        private string _searchLoginTextbox;
        public string SearchLoginTextbox
        {
            get => _searchLoginTextbox;
            set
            {
                if (_searchLoginTextbox == value)
                {
                    return;
                }
                _searchLoginTextbox = value;
                AsynchroneUpdateList();
                OnPropertyChanged("SearchLoginTextbox");
            }
        }

        #region BackgroundWorker List

        protected override object DoWork(BackgroundWorker bgworker)
        {
            int i = 0;
            var usersList = new ObservableCollection<UserDto>();
            foreach (var user in _usersBll.GetUsersList())
            {
                usersList.Add(user);
                i++;
            }
            bgworker?.ReportProgress(i, usersList);
            return usersList;
        }

        protected override void ProgressChanged(ProgressChangedEventArgs e , object list)
        {
            ObservableCollection<UserDto> partialresult;
            if (e == null && list != null)
            {
                partialresult = (ObservableCollection<UserDto>) list;
            }
            else
            {
                partialresult = (ObservableCollection<UserDto>)e.UserState;
            }

            UsersList = new ObservableCollection<UserDto>();
            foreach (var userDto in partialresult)
            {
                UsersList.Add(userDto);
            }
        }

        protected override void RunWorkerCompleted()
        {
            SortUsersList();
            OnPropertyChanged("UsersList");
        }

        #endregion

        public void SortUsersList()
        {
            SortUsersListByLogin();
        }

        private void SortUsersListByLogin()
        {
            if (!string.IsNullOrEmpty(SearchLoginTextbox))
            {
                var sortedList = UsersList.Where(x => x.Login.Contains(SearchLoginTextbox));
                UsersList = new ObservableCollection<UserDto>(sortedList);
            }
        }

        public override void Add(object sender)
        {
            UserEditFormViewModel.Instance.UserDtoToAddOrUpdate = new UserDto();
            UserEditFormViewModel.Instance.IsNewUser = true;
            WindowHelper.ShowDialogWindow<UserEditForm>();
            AsynchroneUpdateList();
        }

        public override void Edit(object sender)
        {
            if (SelectedUser == null)
            {
                return;
            }

            UserEditFormViewModel.Instance.UserDtoToAddOrUpdate = SelectedUser;
            UserEditFormViewModel.Instance.IsNewUser = false;
            WindowHelper.ShowDialogWindow<UserEditForm>();
            AsynchroneUpdateList();
        }

        public override void Delete(object sender)
        {
            BaseDeleteObject("l'utilisateur", SelectedUser, SelectedUser.Login,
                SelectedUser => _usersBll.DeleteUser(SelectedUser));
        }
    }
}
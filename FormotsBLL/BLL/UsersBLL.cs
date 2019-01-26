using System.Collections.ObjectModel;
using FormotsCommon.DTO;
using FormotsDAL;
using FormotsDAL.DAL;

namespace FormotsBLL.BLL
{
    public class UsersBLL
    {
        public ObservableCollection<UserDto> GetUsersList()
        {
            return UsersDAL.GetAllUsers();
        }

        public OperationResult<UserDto> ConnectUser(UserDto user)
        {
            return UsersDAL.ConnectUser(user);
        }

        public OperationResult<UserDto> DeleteUser(UserDto user)
        {
            return UsersDAL.DeleteUser(user);
        }

        public OperationResult<UserDto> AddOrUpdateUser(UserDto user)
        {
            return UsersDAL.AddOrUpdateUser(user);
        }
    }
}
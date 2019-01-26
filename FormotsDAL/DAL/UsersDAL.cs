using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FormotsCommon.DTO;

namespace FormotsDAL.DAL
{
    public class UsersDAL
    {
        public static ObservableCollection<UserDto> GetAllUsers()
        {
            using (var context = new Entities())
            {
                var users = context.global_users.ToList();

                var userDtoList = new ObservableCollection<UserDto>();
                foreach (var userDal in users)
                {
                    var userDto = AutoMapper.Mapper.Map<UserDto>(userDal);
                    userDtoList.Add(userDto);
                }

                return new ObservableCollection<UserDto>(userDtoList);
            }
        }

        public static OperationResult<UserDto> ConnectUser(UserDto user)
        {
            try
            {
                using (var context = new Entities())
                {
                    var bddUser = context.global_users.SingleOrDefault(x => x.Login == user.Login);
                    if (bddUser != null && bddUser.Password.Equals(user.Password))
                    {
                        var userResult = AutoMapper.Mapper.Map<UserDto>(bddUser);
                        return OperationResult<UserDto>.CreateSuccessResult(userResult);
                    }
                }
                return OperationResult<UserDto>.CreateFailure(
                    $"Impossible de connecter l'utilisateur {user.Login} car le mot de passe est erroné.");
            }
            catch (Exception e)
            {
                return OperationResult<UserDto>.CreateFailure(e.Message);
            }
        }

        public static OperationResult<UserDto> DeleteUser(UserDto user)
        {
            try
            {
                using (var context = new Entities())
                {
                    var userToDelete = AutoMapper.Mapper.Map<global_users>(user);
                    context.Entry(userToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                    return OperationResult<UserDto>.CreateSuccessResult(user);
                }
            }
            catch (Exception e)
            {
                return OperationResult<UserDto>.CreateFailure(
                    $"Impossible de supprimer l'utilisateur {user.Login} : {e.Message}");
            }
        }

        public static OperationResult<UserDto> AddOrUpdateUser(UserDto user)
        {
            try
            {
                UserDto savedUserDto;
                using (var context = new Entities())
                {
                    var userToAddOrUpdate = AutoMapper.Mapper.Map<global_users>(user);
                    context.Entry(userToAddOrUpdate).State = userToAddOrUpdate.Id == 0 ? EntityState.Added : EntityState.Modified;

                    context.global_users.AddOrUpdate(userToAddOrUpdate);
                    context.SaveChanges();

                    savedUserDto = AutoMapper.Mapper.Map<UserDto>(userToAddOrUpdate);
                }
                return OperationResult<UserDto>.CreateSuccessResult(savedUserDto);
            }
            catch (Exception e)
            {
                return OperationResult<UserDto>.CreateFailure(e.Message);
            }
        }
    }
}

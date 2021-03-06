using DigiKala.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiKala.Core.ViewModels.User;

namespace DigiKala.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region Account
        bool IsExistUserByEmail(string email);
        void AddUser(User user);
        bool ActiveUserAccount(string activeCode);
        bool IsExistUserByPhoneNumber(string phoneNumber);
        User GetUserByEmail(string email);
        User GetUserByPhoneNumber(string phoneNumber);
        User GetUserById(int userId);
        bool IsExistUserByActivationCode(string activationCode);
        bool ResetUserPassword(string activationCode, string password);
        User IsExistUserForLogin(string email, string password);

        #endregion

        #region UserPanel

        UserInformationsViewModel GetUserInformationsForShow(string email);

        User ConfirmUserInformations(string userEmailOrPhoneNumber, string firstName = "", string lastName = "", string nationalNumber = ""
            , string phoneNumber = "", string email = "", string birthDate ="");
        void ChangeUserPassword(string emailOrPhoneNumber, string password);
        User GetUserByEmailOrPhoneNumber(string emailOrPhoneNumber);
        void UpdateUser(User user);
        #endregion
        #region Admin

        Tuple<List<UsersInformationsForShowInAdminViewModel>,int,int,int> GetUsersInformationsForShowInAdmin(
            int pageId=1,string fullName = "", string email = "", string phoneNumber = "", int take = 10);
        Tuple<List<UsersInformationsForShowInAdminViewModel>, int, int, int> GetDeletedUsersInformationsForShowInAdmin(
            int pageId = 1, string fullName = "", string email = "", string phoneNumber = "", int take = 10);
        void AddUser(CreateUserViewModel createUserVM,int roleId); //Add User From Admin
        void EditUserFromAdmin(EditUserViewModel editUserVM, int roleId);
        void DeleteUser(int userId);
        void ReturnDeletedUser(int userId);
        #endregion
    }
}

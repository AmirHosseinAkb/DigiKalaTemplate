using DigiKala.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserByEmail(string email);
        void AddUser(User user);
        bool ActiveUserAccount(string activeCode);
        bool IsExistUserByPhoneNumber(string phoneNumber);
        User GetUserByEmail(string email);
        User GetUserByPhoneNumber(string phoneNumber);  
    }
}

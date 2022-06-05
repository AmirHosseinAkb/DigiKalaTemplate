using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiKala.Core.Convertors;
using DigiKala.Core.Generators;
using DigiKala.Core.Security;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Core.ViewModels.User;
using DigiKala.Data.Context;
using DigiKala.Data.Entities.User;

namespace DigiKala.Core.Services
{
    public class UserService : IUserService
    {
        public UserService(DigiKalaContext context)
        {
            _context = context;
        }
        private DigiKalaContext _context;

        public bool IsExistUserByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == EmailConvertor.FixEmail(email));
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool ActiveUserAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActivationCode == activeCode);
            if(user==null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActivationCode = NameGenerator.GenerateUniqName();
            _context.SaveChanges();
            return true;
        }

        public bool IsExistUserByPhoneNumber(string phoneNumber)
        {
            return _context.Users.Any(u => u.PhoneNumber == phoneNumber);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == EmailConvertor.FixEmail(email));
        }

        public User GetUserByPhoneNumber(string phoneNumber)
        {
            return _context.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        public bool IsExistUserByActivationCode(string activationCode)
        {
            return _context.Users.Any(u => u.ActivationCode == activationCode);
        }

        public bool ResetUserPassword(string activationCode, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActivationCode == activationCode);
            if (user != null)
            {
                user.Password = PasswordHasher.HashPasswordMD5(password);
                user.ActivationCode = NameGenerator.GenerateUniqName();
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public User IsExistUserForLogin(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == EmailConvertor.FixEmail(email) && u.Password == PasswordHasher.HashPasswordMD5(password));
        }

        public UserInformationsViewModel GetUserInformationsForShow(string email)
        {
            return _context.Users.Where(u => u.Email == EmailConvertor.FixEmail(email))
                .Select(u => new UserInformationsViewModel()
                {
                    Email=u.Email,
                    BirthDate=u.BirthDate,
                    FirstName=u.FirstName,
                    LastName=u.LastName,
                    NationalNumber=u.NationalNumber,
                    PhoneNumber=u.PhoneNumber,
                }).Single();
        }

        public void ConfirmUserInformations(string userEmail,string firstName = "", string lastName = "", string nationalNumber = "", string phoneNumber = "", string email = "", string birthDate = "")
        {
            var user = GetUserByEmail(userEmail);
            if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
            {
                user.FirstName = firstName;
                user.LastName = lastName;
            }
            if (!string.IsNullOrEmpty(nationalNumber))
            {
                user.NationalNumber = nationalNumber;
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                user.PhoneNumber = phoneNumber;
            }
            if (!string.IsNullOrEmpty(email))
            {
                user.Email = EmailConvertor.FixEmail(email);
            }
            if (!string.IsNullOrEmpty(birthDate))
            {
                user.BirthDate = DateTime.Parse(birthDate);
            }
            _context.SaveChanges();    
        }
    }
}

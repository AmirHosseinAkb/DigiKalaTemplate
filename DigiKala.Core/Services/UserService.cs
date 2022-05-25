﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiKala.Core.Convertors;
using DigiKala.Core.Generators;
using DigiKala.Core.Security;
using DigiKala.Core.Services.Interfaces;
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
    }
}

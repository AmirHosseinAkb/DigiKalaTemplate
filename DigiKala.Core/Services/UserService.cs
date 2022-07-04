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
using Microsoft.EntityFrameworkCore;

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
            user.RoleId = _context.Roles.Single(r => r.IsDefaultForNewUsers).RoleId;
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

        public UserInformationsViewModel GetUserInformationsForShow(string emailOrPhoneNumber)
        {
            return _context.Users.Where(u => u.Email == EmailConvertor.FixEmail(emailOrPhoneNumber) 
            || u.PhoneNumber== emailOrPhoneNumber)
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

        public User ConfirmUserInformations(string userEmailOrPhoneNumber, string firstName = "", string lastName = "", string nationalNumber = "", string phoneNumber = "", string email = "", string birthDate = "")
        {
            var user = GetUserByEmailOrPhoneNumber(userEmailOrPhoneNumber);
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
            return user;
        }

        public void ChangeUserPassword(string emailOrPhoneNumber, string password)
        {
            var user = GetUserByEmailOrPhoneNumber(emailOrPhoneNumber);
            user.Password = PasswordHasher.HashPasswordMD5(password);
            _context.SaveChanges(); 
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public User GetUserByEmailOrPhoneNumber(string emailOrPhoneNumber)
        {
            return _context.Users.SingleOrDefault(u => u.Email == EmailConvertor.FixEmail(emailOrPhoneNumber) || u.PhoneNumber == emailOrPhoneNumber);
        }

        public Tuple<List<UsersInformationsForShowInAdminViewModel>, int, int,int> GetUsersInformationsForShowInAdmin(int pageId = 1, string fullName = ""
            , string email = "", string phoneNumber = "", int take = 10)
        {
            if (take < 10)
                take = 10;
            
            IQueryable<User> users = _context.Users.Include(u=>u.Role);
            if (!string.IsNullOrEmpty(fullName))
            {
                users = users.Where(u => (u.FirstName != null && u.FirstName.Contains(fullName)) 
                || (u.LastName!=null && u.LastName.Contains(fullName)));
            }
            if (!string.IsNullOrEmpty(email))
            {
                users = users.Where(u => u.Email!=null && u.Email.Contains(EmailConvertor.FixEmail(email)));
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                users = users.Where(u => u.PhoneNumber != null && u.PhoneNumber.Contains(phoneNumber));
            }
            var skip = (pageId - 1) * take;
            var pageCount = users.Count() / take;
            if (users.Count() % take != 0)
                pageCount++;
            var informations = users.Skip(skip).Take(take)
                .Select(u => new UsersInformationsForShowInAdminViewModel()
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    FullName = u.FirstName + " " + u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    RegisterDate = u.RegisterDate,
                    NationalNumber = u.NationalNumber,
                    AvatarName = u.AvatarName,
                    Role=u.Role
                }).ToList();
            return Tuple.Create(informations, pageId, pageCount,take);
        }

        public void AddUser(CreateUserViewModel createUserVM, int roleId)
        {
            DigiKala.Data.Entities.User.User user = new Data.Entities.User.User()
            {
                ActivationCode = NameGenerator.GenerateUniqName(),
                IsActive = true,
                MessageCode = RandomNumberGenerator.GenerateRendomInteger(10000, 99999),
                RegisterDate = DateTime.Now,
                RoleId = roleId,
                FirstName = createUserVM.FirstName,
                LastName = createUserVM.LastName,
                AvatarName="Default.png",
                Email = createUserVM.Email,
                PhoneNumber = createUserVM.PhoneNumber,
                IsDeleted=false
            };

            if(createUserVM.Password!=null)
                user.Password=PasswordHasher.HashPasswordMD5(createUserVM.Password);

            if (createUserVM.UserAvatar != null)
            {
                if (createUserVM.UserAvatar.FileName != "Default.png")
                {
                    user.AvatarName = NameGenerator.GenerateUniqName()+Path.GetExtension(createUserVM.UserAvatar.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        user.AvatarName);
                    using(var stream=new FileStream(imagePath, FileMode.Create))
                        createUserVM.UserAvatar.CopyTo(stream);
                }
            }
            AddUser(user);
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public void EditUserFromAdmin(EditUserViewModel editUserVM, int roleId)
        {
            var user = GetUserById(editUserVM.UserId);

            if(editUserVM.Email!=user.Email)
                user.Email=EmailConvertor.FixEmail(editUserVM.Email);

            if (editUserVM.PhoneNumber != user.PhoneNumber)
                user.PhoneNumber = editUserVM.PhoneNumber;

            if(!string.IsNullOrEmpty(editUserVM.Password))
                user.Password=PasswordHasher.HashPasswordMD5(editUserVM.Password);

            user.FirstName = editUserVM.FirstName;
            user.LastName=editUserVM.LastName;
            user.RoleId = roleId;
            if (editUserVM.UserAvatar != null)
            {
                string imagePath = "";
                if (user.AvatarName != "Default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        user.AvatarName);
                    if (File.Exists(imagePath))
                        File.Delete(imagePath);
                }
                user.AvatarName = NameGenerator.GenerateUniqName() + Path.GetExtension(editUserVM.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "UserAvatar",
                    user.AvatarName);
                using(var stream=new FileStream(imagePath, FileMode.Create))
                    editUserVM.UserAvatar.CopyTo(stream);   
            }
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            user.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}

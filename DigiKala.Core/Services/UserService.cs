using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiKala.Core.Convertors;
using DigiKala.Core.Services.Interfaces;
using DigiKala.Data.Context;

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
    }
}

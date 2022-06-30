using DigiKala.Core.Services.Interfaces;
using DigiKala.Data.Context;
using DigiKala.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Services
{
    public class PermissionService : IPermissionService
    {
        public PermissionService(DigiKalaContext context)
        {
            _context = context;
        }
        private DigiKalaContext _context;
        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public bool IsExistRoleById(int roleId)
        {
            return _context.Roles.Any(r=>r.RoleId == roleId);   
        }
    }
}

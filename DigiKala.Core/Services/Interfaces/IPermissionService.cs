using DigiKala.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        List<Role> GetAllRoles();
        bool IsExistRoleById(int roleId);
    }
}

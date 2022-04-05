using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
         Task<Role> GetRoleByName(string roleName);
         Task<IEnumerable<Role>> GetSelectedRoles(List<int> RoleIds);
    }
}
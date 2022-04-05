using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
           var role = await _context.Roles.Include(L=> L.UserRoles).ThenInclude(L=> L.User).SingleOrDefaultAsync(L => L.RoleName == roleName && L.IsDeleted == false);
           return role;
        }
        public async Task<Role> GetRole(int Id)
        {
           var role = await _context.Roles.Include(L=> L.UserRoles).ThenInclude(L=> L.User).SingleOrDefaultAsync(L => L.Id == Id && L.IsDeleted == false);
           return role;
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
           var role = await _context.Roles.Include(L=> L.UserRoles).ThenInclude(L=> L.User).Where(L => L.IsDeleted == false).ToListAsync();
           return role;
        }

        public async Task<IEnumerable<Role>> GetSelectedRoles(List<int> RoleIds)
        {
           var role = await _context.Roles.Include(L=> L.UserRoles).ThenInclude(L=> L.User).Where(L => RoleIds.Contains(L.Id)).ToListAsync();
           return role;
        }
    }
}
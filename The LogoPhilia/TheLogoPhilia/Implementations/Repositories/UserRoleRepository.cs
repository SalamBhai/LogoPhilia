using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.Entities.JoinerTables;
using TheLogoPhilia.Interfaces.IRepositories;

namespace TheLogoPhilia.Implementations.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext context) : base(context)
        {
        }

        public async  Task<List<UserRole>> GetUserRoleByUserId(int UserId)
        {
           return await _context.UserRoles.Include(L=> L.User).Include(L=> L.Role).Where(L=>L.UserId == UserId).ToListAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;

namespace TheLogoPhilia.Implementations.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
           return await _context.Users.Include(L=> L.ApplicationAdministrator).Include(L=> L.ApplicationUser).ThenInclude(L=> L.User).ThenInclude(L=> L.UserRoles).ThenInclude(L=> L.Role).Where(L=> L.IsDeleted == false).ToListAsync();
        }

        public async Task<User> GetUser(int Id)
        {
          return await _context.Users.Include(L=> L.ApplicationAdministrator).Include(L=> L.ApplicationUser).Include(L=> L.UserRoles).ThenInclude(L=> L.Role).SingleOrDefaultAsync(L=> L.Id ==Id && L.IsDeleted == false);
        }

        public async Task<User> GetUser(Expression<Func<User, bool>> expression)
        {
           return await _context.Users.Include(L=> L.ApplicationAdministrator).Include(L=> L.ApplicationUser).Include(L=> L.UserRoles).ThenInclude(L=> L.Role).SingleOrDefaultAsync(expression);
        }
    }
}
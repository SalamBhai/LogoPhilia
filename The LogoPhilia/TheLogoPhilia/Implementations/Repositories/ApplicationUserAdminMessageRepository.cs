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
    public class ApplicationUserAdminMessageRepository : BaseRepository<ApplicationUserAdminMessage>, IApplicationUserAdminMessageRepository
    {
        
        public ApplicationUserAdminMessageRepository(AppDbContext context) : base(context)
        {
            _context= context;
        }

        public async Task<IEnumerable<ApplicationUserAdminMessage>> GetApplicationUserAdminMessageToUser(int UserId)
        {
            return await _context.ApplicationUserAdminMessages.Include(N=> N.ApplicationUser).Include(L=> L.AdministratorMessage).Where(N=>N.ApplicationUserId==UserId).ToListAsync();
        }
        
    }
}
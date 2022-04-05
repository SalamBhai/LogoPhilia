using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;

namespace TheLogoPhilia.Implementations.Repositories
{
    public class ApplicationAdministratorRepository : BaseRepository<ApplicationAdministrator>, IApplicationAdministratorRepository
    {
         public ApplicationAdministratorRepository(AppDbContext context) : base(context)
        {
            _context= context;
        }

        public async Task<IEnumerable<ApplicationAdministrator>> GetAllApplicationAdministrators()
        {
            return await _context.ApplicationAdministrators.Include(L => L.User).ThenInclude(L=> L.UserRoles).ThenInclude(L=> L.Role).Where(L=> L.AdministratorType == AdminType.Administrator && L.IsDeleted==false).ToListAsync();
        }

        public async Task<ApplicationAdministrator> GetApplicationAdministrator(int Id)
        {
          return await _context.ApplicationAdministrators.Include(L => L.User).ThenInclude(L=> L.UserRoles).ThenInclude(L=> L.Role).Where(L=> L.IsDeleted==false).SingleOrDefaultAsync(L=> L.Id==Id);
        }
        public async Task<IEnumerable<ApplicationAdministrator>> GetAllApplicationSubAdministrator(int Id)
        {
          return await _context.ApplicationAdministrators.Include(L => L.User).ThenInclude(L=> L.UserRoles).ThenInclude(L=> L.Role).Where(L=> L.IsDeleted==false && L.AdministratorType == AdminType.SubAdministrator).ToListAsync();
        }
    }
}
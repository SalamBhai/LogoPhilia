using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;

namespace TheLogoPhilia.Implementations.Repositories
{
    public class AdministratorMessageRepository  :BaseRepository<AdministratorMessage>, IAdministratorMessageRepository
    {
       
        public AdministratorMessageRepository(AppDbContext context) : base(context)
        {
            _context= context;
        }

        public async Task<AdministratorMessage> GetAdminMessage(int Id)
        {
           var adminMessage = await _context.AdministratorMessages.Include(L=> L.ApplicationUserAdminMessages).SingleOrDefaultAsync(L=> L.Id ==Id);
           return adminMessage;

        }

        public async Task<IEnumerable<AdministratorMessage>> GetAdminMessages()
        {
            return await _context.AdministratorMessages.Include(L=> L.ApplicationUserAdminMessages).ToListAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;

namespace TheLogoPhilia.Implementations.Repositories
{
    public class NotesRepository : BaseRepository<Notes>, INotesRepository
    {
        public NotesRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notes>> GetNotesOfUser(int UserId)
        {
            return await _context.Notes.Include(N=> N.ApplicationUser).ThenInclude(L=> L.User).Where(N=>N.ApplicationUserId==UserId && N.IsDeleted== false).ToListAsync();
        }
        public async Task<IEnumerable<Notes>> GetNotes()
        {
            return await _context.Notes.Include(N=> N.ApplicationUser).ThenInclude(L=> L.User).Where(N=>N.IsDeleted == false).ToListAsync();
        }
        public async Task<Notes> GetNote(int Id)
        {
            return await _context.Notes.Include(N=> N.ApplicationUser).ThenInclude(L=> L.User).Where(N=>N.Id==Id && N.IsDeleted == false).SingleOrDefaultAsync();
        }
    }
}
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
    public class ApplicationUserCommentRepository : BaseRepository<ApplicationUserComment>, IApplicationUserCommentRepository
    {
        public ApplicationUserCommentRepository(AppDbContext context) : base(context)
        {
            
        }
         public async Task<ApplicationUserComment> GetApplicationUserComment(int id)
        {
          return await _context.ApplicationUserComments.Include(L=> L.ApplicationUser).ThenInclude(L=> L.User).Include(L=> L.Post).SingleOrDefaultAsync(L=> L.Id ==id);
        }

        public async Task<IEnumerable<ApplicationUserComment>> GetApplicationUserComments()
        {
            return await _context.ApplicationUserComments.Include(L=> L.ApplicationUser).ThenInclude(L=> L.User).Include(L=> L.Post).ToListAsync();
        }
        public async Task<IEnumerable<ApplicationUserComment>> GetCommentsOfAPost(int PostId)
        {
            return await _context.ApplicationUserComments.Include(L=> L.ApplicationUser).ThenInclude(L=> L.User).Include(L=> L.Post).Where(L=> L.PostId == PostId && L.IsDeleted == false).ToListAsync();
        }
    }
}
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
    public class ApplicationUserPostRepository : BaseRepository<ApplicationUserPost>, IApplicationUserPostRepository
    {
        public ApplicationUserPostRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUserPost>> GetAllPosts()
        {
          return await _context.ApplicationUserPosts.Include(L=> L.ApplicationUser).ThenInclude(L=> L.User).Include(L=> L.ApplicationUserComments).Include(L=> L.PostLog).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUserPost>> GetAllPosts(Expression<Func<ApplicationUserPost, bool>> expression)
        {
           return await _context.ApplicationUserPosts.Include(L=> L.ApplicationUser).ThenInclude(L=> L.User).Include(L=> L.ApplicationUserComments).Include(L=> L.PostLog).Where(expression).ToListAsync();
        }

        public async Task<ApplicationUserPost> GetApplicationUserPost(int id)
        {
          return await _context.ApplicationUserPosts.Include(L=> L.ApplicationUser).ThenInclude(L=>L.User).Include(L=> L.ApplicationUserComments).Include(L=> L.PostLog).SingleOrDefaultAsync(L=> L.Id ==id);
        }
    }
}
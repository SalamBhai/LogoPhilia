using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.ApplicationDbContext;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;

namespace TheLogoPhilia.Implementations.Repositories
{
    public class PostLogRepository : BaseRepository<PostLog>, IPostLogRepository
    {
        public PostLogRepository(AppDbContext context) : base(context)
        {
        }

        public async  Task<IEnumerable<PostLog>> GetLogsOfPostCreatedToday()
        {
            return await _context.PostLogs.Include(L=> L.ApplicationUserPost).Where(L=> L.ApplicationUserPost.DatePosted.Date == System.DateTime.UtcNow.Date).ToListAsync();
        }
    }
}
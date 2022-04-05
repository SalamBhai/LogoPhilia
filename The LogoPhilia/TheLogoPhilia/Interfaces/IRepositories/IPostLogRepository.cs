using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IPostLogRepository : IRepository<PostLog>
    {
        Task<IEnumerable<PostLog>> GetLogsOfPostCreatedToday();
    }
}
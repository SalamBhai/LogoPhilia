using TheLogoPhilia.Entities.JoinerTables;
using System;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using System.Collections.Generic;
using TheLogoPhilia.Implementations.Repositories;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IApplicationUserAdminMessageRepository : IRepository<ApplicationUserAdminMessage>
    {
       Task<IEnumerable<ApplicationUserAdminMessage>> GetApplicationUserAdminMessageToUser(int UserId);
    }
}
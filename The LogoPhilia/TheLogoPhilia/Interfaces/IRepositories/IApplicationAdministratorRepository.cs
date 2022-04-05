using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IApplicationAdministratorRepository : IRepository<ApplicationAdministrator>
    {
         Task<ApplicationAdministrator> GetApplicationAdministrator( int Id);
         Task<IEnumerable<ApplicationAdministrator>> GetAllApplicationAdministrators();
    }
}
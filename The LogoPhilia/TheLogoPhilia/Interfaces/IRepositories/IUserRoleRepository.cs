using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
          Task<List<UserRole>> GetUserRoleByUserId(int UserId);
    }
}
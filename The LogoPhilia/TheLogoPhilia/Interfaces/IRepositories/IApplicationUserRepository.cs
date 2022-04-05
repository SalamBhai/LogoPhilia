using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUser(int id);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();  
        Task<IEnumerable<ApplicationUser>> GetSelectedApplicationUsers(List<int> UserIds);  
        Task<IEnumerable<ApplicationUser>> GetBirthDayUsers();
        Task<IEnumerable<string>> GetUserEmails();
         Task<ApplicationUser> GetUserByExpression(Expression<Func<ApplicationUser, bool>> expression);
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(int Id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(Expression<Func<User, bool>> expression);
    }
}
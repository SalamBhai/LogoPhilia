using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IApplicationUserCommentRepository : IRepository<ApplicationUserComment>
    {
        Task<ApplicationUserComment> GetApplicationUserComment(int Id);
        Task<IEnumerable<ApplicationUserComment>> GetApplicationUserComments();
        Task<IEnumerable<ApplicationUserComment>> GetCommentsOfAPost(int PostId);
    }
}
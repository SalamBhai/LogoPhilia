using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface ILanguageOfOriginRepository : IRepository<LanguageOfOrigin>
    {
         Task<LanguageOfOrigin> GetLanguageOfOrigin(int Id);
         Task<IEnumerable<LanguageOfOrigin>> GetLanguageOfOrigins();
         Task<IEnumerable<LanguageOfOriginViewModel>> Search(string SearchText);
    }
}
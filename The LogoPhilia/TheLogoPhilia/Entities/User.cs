using System.Collections.Generic;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.Entities
{
    public class User : BaseEntity
    {
        public string UserName{get;set;}
        public string Email{get;set;}
        public string Password{get;set;}
        public ICollection<UserRole> UserRoles{get;set;} = new List<UserRole>();
        public ApplicationUser ApplicationUser{get;set;}
        public ApplicationAdministrator ApplicationAdministrator{get;set;}
        public string NormalPassword{get;set;}
    }
}
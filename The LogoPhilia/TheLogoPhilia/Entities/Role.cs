using System.Collections.Generic;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.Entities
{
    public class Role: BaseEntity
    {
        public string RoleName{get;set;}
        public ICollection<UserRole> UserRoles{get;set;}
        
    }
}
using System;
using System.Collections.Generic;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.Models
{
    public class UserViewModel
    {
        public int Id {get;set;}
        public string UserName{get;set;}
        public string Email{get;set;}
        public ICollection<RoleViewModel> UserRoles{get;set;}
        public string ApplicationUserFullName{get;set;}
        public string  ApplicationAdministratorFullName{get;set;}
    }
    public class UpdateUserRequestModel
    {
        public string UserName{get;set;}
        public ICollection<UserRole> UserRoles{get;set;}
        public string ApplicationUserFullName{get;set;}
        public string  ApplicationAdministratorFullName{get;set;}
    }
}
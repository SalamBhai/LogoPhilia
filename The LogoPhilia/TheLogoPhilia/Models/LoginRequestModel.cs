using System.Collections.Generic;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.Models
{
    public class LoginUserRequestModel
    {
        public string UserName{get;set;}
        public string Email{get;set;}
        public string Password{get;set;}
        
    } 
    public class LoginAdministratorRequestModel
    {
        public string UserName{get;set;}
        public string Email{get;set;}
        public string Password{get;set;}
        public string AdministratorCode{get;set;}
        
    } 
    public class LoginResponseModel
    {
        public string Name{get;set;}
      public string Email{get;set;}
      public List<RoleViewModel> UserRoles{get;set;}
      public string Token{get;set;}
    }
     
}
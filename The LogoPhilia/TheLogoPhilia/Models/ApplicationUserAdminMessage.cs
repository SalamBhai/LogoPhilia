using System;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Models
{
    public class ApplicationUserAdminMessageViewModel 
    {
        public int Id{get;set;}
        public int ApplicationUserId {get;set;}
        public string ApplicationUserName{get;set;}
        public int AdministratorMessageId{get;set;}
        public string AdministratorMessage{get;set;}
    }
}
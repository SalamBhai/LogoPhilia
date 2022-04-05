using System;

namespace TheLogoPhilia.Entities.JoinerTables
{
    public class ApplicationUserAdminMessage : BaseEntity
    {
        public int ApplicationUserId {get;set;}
        public ApplicationUser ApplicationUser{get;set;}
        public int AdministratorMessageId{get;set;}
        public AdministratorMessage AdministratorMessage{get;set;}
    }
}
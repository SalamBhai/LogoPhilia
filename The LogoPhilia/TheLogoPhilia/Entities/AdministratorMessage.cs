using System;
using System.Collections.Generic;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.Entities
{
    public class AdministratorMessage : BaseEntity
    {
        public string MessageContent{get;set;}
        public MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}
        public DateTime DateSent{get;set;}
        public ICollection<ApplicationUserAdminMessage> ApplicationUserAdminMessages{get;set;}
    }
}
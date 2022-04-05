using System;
using System.Collections.Generic;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Entities.JoinerTables;


namespace TheLogoPhilia.Models
{
    public class AdministratorMessageViewModel 
    {
        public int Id{get;set;}
        public string MessageContent{get;set;}
        public MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}
        public DateTime DateSent{get;set;}
        public ICollection<ApplicationUserAdminMessageViewModel> ApplicationUserAdminMessages{get;set;}
    }
    public class CreateAdministratorMessageRequestModel 
    {
        public MessageType MessageType{get;set;}
        // public int ApplicationUserId{get;set;}
        public string MessageSubject{get;set;}

    }
    public class UpdateAdministratorMessageRequestModel 
    {
    
        public MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}
        public DateTime DateSent{get;set;}
        
    }
}
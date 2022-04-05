using System;
using TheLogoPhilia.ApplicationEnums;

namespace TheLogoPhilia.Entities
{
    public class ApplicationAdministrator: BaseEntity
    {
        public string FirstName {get;set;}
        public string LastName{get;set;}
        public string AdministratorImage{get;set;}
        public string AdministratorEmail{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
        public int Age {get;set;}
        public DateTime DateOfBirth{get;set;}
        public AdminType AdministratorType{get;set;}
    
        public string AdministratorCode{get;set;}
    }
}
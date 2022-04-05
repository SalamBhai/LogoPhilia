using System;
using System.Collections.Generic;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Entities.JoinerTables;


namespace TheLogoPhilia.Entities
{
    public class ApplicationUser :BaseEntity
    {
        public string FirstName {get;set;}
        public string LastName{get;set;}
        public string UserEmail{get;set;}
        public string ApplicationUserImage{get;set;}
        public Gender Gender {get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
        public int Age {get;set;}
        public DateTime DateOfBirth{get;set;}
        public string Country{get;set;}

        public ICollection<ApplicationUserComment> ApplicationUserComments{get;set;}  = new List<ApplicationUserComment>();
        public ICollection<ApplicationUserPost> ApplicationUserPosts{get;set;}  = new List<ApplicationUserPost>();

    
           public ICollection<ApplicationUserAdminMessage> ApplicationUserAdminMessages{get;set;} = new List<ApplicationUserAdminMessage>();
           public ICollection<Notes> ApplicationUserNotes {get;set;}  = new List<Notes>();


    }
}
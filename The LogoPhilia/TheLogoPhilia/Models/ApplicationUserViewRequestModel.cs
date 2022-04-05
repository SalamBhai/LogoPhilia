using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.Models
{
    public class ApplicationUserViewRequestModel
    {
        public int ApplicationUserId{get;set;}
        public string FullName {get;set;}
        public string UserName{get;set;}

        public string UserEmail{get;set;}
        public string ApplicationUserImage{get;set;}
        public Gender Gender {get;set;}
        public int UserId{get;set;}

        public int Age {get;set;}
        public DateTime DateOfBirth{get;set;}
        public string Country{get;set;}

        public ICollection<ApplicationUserCommentViewModel> ApplicationUserComments{get;set;}  = new List<ApplicationUserCommentViewModel>();
        public ICollection<ApplicationUserPostViewModel> ApplicationUserPosts{get;set;}  = new List<ApplicationUserPostViewModel>();
       public ICollection<ApplicationUserAdminMessageViewModel> ApplicationUserAdminMessages{get;set;} = new List<ApplicationUserAdminMessageViewModel>();
       public ICollection<NotesViewModel> ApplicationUserNotes {get;set;}  = new List<NotesViewModel>(); 
    }
    public class ApplicationUserCreateRequestModel
    {
         public string FirstName {get;set;}
        public string LastName{get;set;}
        public string UserEmail{get;set;}
        public string UserImage {get;set;}
        public string UserName{get;set;}
        public Gender Gender {get;set;}
        public DateTime DateOfBirth{get;set;}
        public string Country{get;set;}
        public string Password{get;set;}
    }
    public class ApplicationUserUpdateRequestModel
    {
         public string UserImage {get;set;}
          public string FirstName {get;set;}
        public string LastName{get;set;}
        public DateTime DateOfBirth{get;set;}
        public string Country{get;set;}
        public string UserName{get;set;}
       
    }
}
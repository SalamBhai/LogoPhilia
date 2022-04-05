using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TheLogoPhilia.Models
{
    public class ApplicationUserPostViewModel
    {
        public int PostId{get;set;}
        public int ApplicationUserId{get;set;}

        public string ApplicationUserEmail{get;set;}
        public string ApplicationUserName{get;set;}
        public string PostContent{get;set;}
        public string VideoFile{get;set;}
        public DateTime DatePosted{get;set;}
        public ICollection<ApplicationUserCommentViewModel> ApplicationUserComments{get;set;} = new List<ApplicationUserCommentViewModel>();
   
    }

    public class CreateApplicationUserPostViewModel
    {
        
        public string PostContent{get;set;}
        public IFormFile VideoFile {get;set;}
    }

    public class UpdateApplicationUserPostViewModel
    {
        public string PostContent{get;set;}
    
    }
}
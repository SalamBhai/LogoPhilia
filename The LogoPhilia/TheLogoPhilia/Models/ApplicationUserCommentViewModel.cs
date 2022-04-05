using System;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Models
{
    public class ApplicationUserCommentViewModel 
    {
        public int Id{get;set;}
        public int ApplicationUserId{get;set;}
        public string ApplicationUserName{get;set;}
        public string CommentContent{get;set;}
        public int PostId{get;set;}
        public string PostCreator{get;set;}
        public DateTime PostDate{get;set;}
        public DateTime CommentDate{get;set;}
    }
    public class CreateApplicationUserCommentModel 
    {
        public string CommentContent{get;set;}
        public int PostId{get;set;}
    }
    public class UpdateApplicationUserCommentModel 
    {
        public string CommentContent{get;set;}
    }
}
using System;

namespace TheLogoPhilia.Entities
{
    public class ApplicationUserComment : BaseEntity
    {
        public int ApplicationUserId{get;set;}
        public ApplicationUser ApplicationUser{get;set;}
        public string CommentContent{get;set;}
        public int PostId{get;set;}
        public ApplicationUserPost Post{get;set;}
        public DateTime CommentDate{get;set;}
    }
}
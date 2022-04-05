using System;
using System.Collections.Generic;

namespace TheLogoPhilia.Entities
{
    public class ApplicationUserPost : BaseEntity
    {
        public int ApplicationUserId{get;set;}
        public ApplicationUser ApplicationUser{get;set;}
        public string PostContent{get;set;}
        public string VideoFile{get;set;}
        public DateTime DatePosted{get;set;}
        public ICollection<ApplicationUserComment>ApplicationUserComments{get;set;}
        public PostLog PostLog {get;set;}
    }
}
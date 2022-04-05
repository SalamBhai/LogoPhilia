using System;

namespace TheLogoPhilia.Entities
{
    public class PostLog :BaseEntity
    {
        public int ApplicationUserPostId{get;set;}
        public ApplicationUserPost ApplicationUserPost{get;set;}
        public string PostUrl{get;set;}
    }
}
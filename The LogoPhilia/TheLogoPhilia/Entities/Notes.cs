using System;


namespace TheLogoPhilia.Entities
{
    public class Notes : BaseEntity
    {
        public int ApplicationUserId{get;set;}
        public ApplicationUser ApplicationUser{get;set;}
        public string Content{get;set;}
        public DateTime DateAdded{get;set;}

    }
}
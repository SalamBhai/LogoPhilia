using System;


namespace TheLogoPhilia.Entities
{
    public abstract class BaseEntity 
    {
        public int Id {get;set;}
        public string CreatedBy {get;set;} 
        public string ModifiedBy {get;set;}
        public DateTime DateCreated {get;set;} 
        public DateTime DateModified{get;set;} 
        public bool IsDeleted{get;set;}
    }
}
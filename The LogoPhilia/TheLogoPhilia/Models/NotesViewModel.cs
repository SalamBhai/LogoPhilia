using System;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Models
{
    public class NotesViewModel
    {
        public int ApplicationUserId{get;set;}
        public int NoteId{get;set;}
        public string ApplicationUserUserName{get;set;}
        public string Content{get;set;}
        public DateTime DateAdded{get;set;}

    }
    public class CreateNotesRequestModel
    {
   
        // public int ApplicationUserId{get;set;}
        public string Content{get;set;}
    }
    public class UpdateNotesRequestModel
    {
        public int ApplicationUserId{get;set;}
        public string Content{get;set;}
        public DateTime DateAdded{get;set;}

    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using System.Linq;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IUserRepository _userRepository;

        public NotesService(INotesRepository notesRepository, IUserRepository userRepository)
        {
            _notesRepository = notesRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<NotesViewModel>> Create(CreateNotesRequestModel model,  int UserId)
        {
            var userInPost = await _userRepository.GetUser(UserId);
            var note = new Notes
            {
                Content = model.Content,
                 DateAdded = DateTime.UtcNow,
                 ApplicationUser =userInPost.ApplicationUser,
                 ApplicationUserId = userInPost.ApplicationUser.Id,

                
            };
           await _notesRepository.Create(note);
            return new BaseResponse<NotesViewModel>
            {
                Message= "Registration Of Notes Successful",
                Success = true,
                Data = new NotesViewModel
                {
                   NoteId = note.Id,
                   DateAdded = note.DateAdded,
                   ApplicationUserId = note.ApplicationUserId,
                }
            };
        }

        public async Task<BaseResponse<NotesViewModel>> Update(UpdateNotesRequestModel model,  int Id)
        {
           var note = await _notesRepository.Get(Id);
           if(note == null) return new BaseResponse<NotesViewModel>
           {
               Message= "Not Found",
               Success = false,
           };
               note.Content = model.Content;
               note.DateAdded = model.DateAdded;
               return new BaseResponse<NotesViewModel>
               {
                  Message = "Update Success",
                  Success = true,
                  Data = new NotesViewModel
                  {
                  NoteId = note.Id,
                  ApplicationUserId = note.ApplicationUserId,
                  Content = note.Content,
                  DateAdded = note.DateAdded,
                  }
               };
        }

        public async Task<BaseResponse<NotesViewModel>> Get( int Id)
        {
             var note = await _notesRepository.Get(Id);
             if(note == null) return new BaseResponse<NotesViewModel>
             {
                 Message= "failure to find notes",
                 Success = false,
             };
            return new BaseResponse<NotesViewModel>
               {
                  Message = "Update Success",
                  Success = true,
                  Data = new NotesViewModel
                  {
                  NoteId = note.Id,
                  ApplicationUserId = note.ApplicationUserId,
                    Content = note.Content,
                    DateAdded = note.DateAdded,
                  }
               };            
        }
          public async Task<BaseResponse<IEnumerable<NotesViewModel>>> GetNotesByApplicationUser( int UserId)
          {
               var userInPost = await _userRepository.GetUser(UserId);
                var appUserId= userInPost.ApplicationUser.Id;
              var userNotes = await _notesRepository.GetNotesOfUser(appUserId);
             
              if(userNotes == null) return new BaseResponse<IEnumerable<NotesViewModel>>{
                  Message = "Retrieval Failed",
                  Success = false,              
                };
               
                return new BaseResponse<IEnumerable<NotesViewModel>>
                {
                  Message = "Retrieval Successful",
                  Success = true,
                   Data= userNotes.Select(L=> new NotesViewModel
                   {
                     ApplicationUserId = L.ApplicationUserId,
                      Content = L.Content,
                     NoteId = L.Id,
                     DateAdded = L.DateAdded,
                     ApplicationUserUserName = L.ApplicationUser.User.UserName,
                   }).ToList()
                };
          }
         
         public async Task<BaseResponse<IEnumerable<NotesViewModel>>> GetNotes()
         {
             var notes = await _notesRepository.GetNotes();
             if(notes == null) return new BaseResponse<IEnumerable<NotesViewModel>>
             {
                 Message= "Failure To Get All",
                 Success= false,
             };
             var notesReturned = notes.Select(L=> new NotesViewModel
             {
                ApplicationUserId = L.ApplicationUserId,
                ApplicationUserUserName = L.ApplicationUser.User.UserName,
                Content = L.Content,
                NoteId = L.Id,
                DateAdded = L.DateAdded,
             }).ToList();
             return new BaseResponse<IEnumerable<NotesViewModel>>
             {
                 Message =" Notes Here",
                 Success = true,
                 Data = notesReturned,
             };
         }

        public async Task<bool> Delete( int Id)
        {
          var note = await _notesRepository.Get(Id);
          if(note == null) return false;
          note.IsDeleted =true;
          _notesRepository.SaveChanges();
          return true;
        }
    }
}
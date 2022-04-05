using System;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;
using System.Collections.Generic;
namespace TheLogoPhilia.Interfaces.IServices
{
    public interface INotesService
    {
         Task<BaseResponse<NotesViewModel>> Create(CreateNotesRequestModel model, int UserId);
         Task<BaseResponse<NotesViewModel>> Get(int Id);
         Task<BaseResponse<NotesViewModel>> Update(UpdateNotesRequestModel model, int Id);
         Task<BaseResponse<IEnumerable<NotesViewModel>>> GetNotesByApplicationUser(int UserId);
         Task<BaseResponse<IEnumerable<NotesViewModel>>> GetNotes();
         
        
        Task<bool> Delete(int Id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;


namespace TheLogoPhilia.Interfaces.IServices
{
    public interface IApplicationUserCommentService
    {
          Task<BaseResponse<ApplicationUserCommentViewModel>> CreateComment(CreateApplicationUserCommentModel model,int UserId);
        Task<BaseResponse<ApplicationUserCommentViewModel>> UpdateComment(UpdateApplicationUserCommentModel model, int Id);
        Task<BaseResponse<ApplicationUserCommentViewModel>> Get(int Id);
        Task<BaseResponse<IEnumerable<ApplicationUserCommentViewModel>>> Get();
       Task<BaseResponse<IEnumerable<ApplicationUserCommentViewModel>>> GetCommentsOfAPost(int PostId);
        Task<bool> Delete(int Id);
    }
}
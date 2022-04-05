using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using TheLogoPhilia.ApplicationEnums;

namespace TheLogoPhilia.Interfaces.IServices
{
    public interface IAdministratorMessageService   
    {
         Task<BaseResponse<AdministratorMessageViewModel>> CreateAdministratorMessageForDiscussionToMany(CreateAdministratorMessageRequestModel model);
         Task<BaseResponse<AdministratorMessageViewModel>> CreateAdministratorMessageToUser(CreateAdministratorMessageRequestModel model, int UserId);
         Task<BaseResponse<AdministratorMessageViewModel>> CreateAdministratorMessageToBirthdayUsers(CreateAdministratorMessageRequestModel model);
         Task<BaseResponse<AdministratorMessageViewModel>> CreateAdministratorMessageToManyUsers(CreateAdministratorMessageRequestModel model);
        Task<BaseResponse<AdministratorMessageViewModel>> UpdateAdministratorMessage(UpdateAdministratorMessageRequestModel model,int Id);
        Task<BaseResponse<IEnumerable<ApplicationUserAdminMessageViewModel>>> GetAdminMessagesToAUser(int UserId);
        Task<BaseResponse<AdministratorMessageViewModel>> Get(int Id);
        Task<BaseResponse<IEnumerable<AdministratorMessageViewModel>>> Get();
        Task<bool> Delete(int Id);

    }
}
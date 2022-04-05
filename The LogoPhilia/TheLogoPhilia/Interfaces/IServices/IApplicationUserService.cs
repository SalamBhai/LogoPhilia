using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IServices
{
      public interface IApplicationUserService
      {
          Task<BaseResponse<ApplicationUserViewRequestModel>> CreateApplicationUser(ApplicationUserCreateRequestModel model);
        Task<BaseResponse<ApplicationUserViewRequestModel>> UpdateApplicationUser(ApplicationUserUpdateRequestModel model,int Id);
        Task<BaseResponse<ApplicationUserViewRequestModel>> Get(int Id);
        Task<BaseResponse<IEnumerable<ApplicationUserViewRequestModel>>> Get();
         Task<bool> Delete(int Id);
      }
}
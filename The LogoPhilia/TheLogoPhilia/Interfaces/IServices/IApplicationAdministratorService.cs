using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IServices
{
    public interface IApplicationAdministratorService 
    {
         Task<BaseResponse<ApplicationAdministratorViewModel>> CreateApplicationAdministrator(CreateApplicationAdministratorRequestModel model);
        Task<BaseResponse<ApplicationAdministratorViewModel>> UpdateApplicationAdministrator(UpdateApplicationAdministratorRequestModel model, int Id);
        Task<BaseResponse<ApplicationAdministratorViewModel>> Get(int Id);
        Task<BaseResponse<IEnumerable<ApplicationAdministratorViewModel>>> Get();
        Task<BaseResponse<ApplicationAdministratorViewModel>> CreateSubAdministrator(CreateSubAdministratorRequestModel model );
       Task<bool> Delete(int Id);
    }
}
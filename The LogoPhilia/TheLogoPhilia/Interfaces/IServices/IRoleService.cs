using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IServices
{
    public interface IRoleService
    {
         Task<BaseResponse<RoleViewModel>> Create(CreateRoleRequestModel model);
         Task<BaseResponse<RoleViewModel>> Get(int Id);
       Task<BaseResponse<IEnumerable<RoleViewModel>>> Get();
         Task<BaseResponse<RoleViewModel>> Update(UpdateRoleRequestModel model, int Id);
         Task<BaseResponse<RoleViewModel>> GetRoleByName(string RoleName);
         Task<bool> Delete(int Id);
    }
}
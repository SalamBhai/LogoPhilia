using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IServices
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetAll();
        Task<BaseResponse<UserViewModel>> Get(int Id);
        Task<BaseResponse<UserViewModel>> LoginApplicationUser(LoginUserRequestModel model);
        Task<BaseResponse<UserViewModel>> LoginApplicationAdministrator(LoginAdministratorRequestModel model);
        Task<bool> Delete(int Id);
    }
}
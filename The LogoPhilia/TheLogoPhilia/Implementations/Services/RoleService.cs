using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository; 
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository= roleRepository;
        }       
        public async Task<BaseResponse<RoleViewModel>> Create(CreateRoleRequestModel model)
        {
          var determineRoleExist = _roleRepository.AlreadyExists(L=> L.RoleName == model.RoleName);
          if(determineRoleExist) 
          {
              return new BaseResponse<RoleViewModel>
             {
                Message ="Role Creation Unsuccessful Because Role Already exists",
                Success = false
             };
          }
          var role = new Role 
          {
            RoleName = model.RoleName,
          };
           await  _roleRepository.Create(role);
           return new BaseResponse<RoleViewModel>
           {
               Message = "Role Added Successfully",
               Data = new RoleViewModel
               {
                   Id = role.Id,
                   RoleName = role.RoleName,
               },
               Success= true,
           };
        }
        public async Task<BaseResponse<RoleViewModel>> Get( int Id)
        {
            var role = await _roleRepository.Get(Id);
            if(role== null)  return new BaseResponse<RoleViewModel>{
               Message =$"Role With Id  {Id} Not Found",
              Success = false
            };
            
            return new BaseResponse<RoleViewModel>
            {
                  Message = "Role Here",
                Data = new RoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                 
                },
                Success = true,
               
            };
        }

        public async Task<BaseResponse<RoleViewModel>> GetRoleByName(string RoleName)
        {
           var role =  await _roleRepository.Get(L=> L.RoleName == RoleName);
           if(role== null)  return new BaseResponse<RoleViewModel>{
               Message =$"Role With Id  {RoleName} Not Found",
              Success = false
            };
           return new BaseResponse<RoleViewModel>
           {
               Message = "Here Is The Role",
               Data = new RoleViewModel
               {
                   Id = role.Id,
                   RoleName = role.RoleName,
               },
               Success = true,
           };
        }

        public async Task<BaseResponse<RoleViewModel>> Update(UpdateRoleRequestModel model,  int Id)
        {
            var role =  await _roleRepository.Get(Id);
            if(role == null)  return new BaseResponse<RoleViewModel>
            {
               Message =$"Role With Id  {Id} Not Found",
               Success = false
            };
            role.RoleName = model.RoleName;
            await _roleRepository.Update(role);

            return new BaseResponse<RoleViewModel>
           {
               Message = $"Role With Id  {Id} Successfully Updated",
               Data = new RoleViewModel
               {
                   Id = role.Id,
                   RoleName = role.RoleName,
               },
               Success = true,
           };
        }

        public async Task<bool> Delete( int Id)
        {
           var role= await _roleRepository.Get(Id);
           if(role == null) return false;
           role.IsDeleted = true;
           return true;
        }

        public async Task<BaseResponse<IEnumerable<RoleViewModel>>> Get()
        {
           var roles = await _roleRepository.Get();
           var rolesReturned = roles.Select(L => new RoleViewModel{
                 Id = L.Id,
                 RoleName =L.RoleName,
           }).ToList();
           return new BaseResponse<IEnumerable<RoleViewModel>>
           {
                Message = "Successful Retrieval",
                Success = true,
                Data = rolesReturned,
           };
        }
    }
}
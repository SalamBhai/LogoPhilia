using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserService(IUserRepository userRepository,IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository =  userRoleRepository;
        }

        public async  Task<bool> Delete( int Id)
        {
          var user = await _userRepository.Get(Id);
          if(user==null) return false;
          user.IsDeleted = true;
          _userRepository.SaveChanges();
          return true;
        }

        public async Task<BaseResponse<UserViewModel>> Get( int Id)
        {
           var user = await _userRepository.GetUser(Id);
           var userRoles = await _userRoleRepository.GetUserRoleByUserId(user.Id);
           if(user==null) return new BaseResponse<UserViewModel>
           {
               Message ="User Retrieval Unsuccessful",
               Success= false,
           };
           return new BaseResponse<UserViewModel>
           {
              Message = "User Retrieval Successful",
              Success  = true,
              Data = new UserViewModel
              {
                  ApplicationUserFullName = user.UserName,
                  Id = user.Id,
                  Email = user.Email,
                  UserName = user.UserName,
                  UserRoles = user.UserRoles.Select(L=> new RoleViewModel
                  {
                     Id = L.Role.Id,
                      RoleName = L.Role.RoleName,
                  }).ToList(),
              }
           };
           
        }

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetAll()
        {
           var allUsers = await _userRepository.GetAllUsers();
           var allUsersReturned = allUsers.Select(user=> new UserViewModel
           {
               ApplicationUserFullName = user.UserName,
                  Id = user.Id,
                  Email = user.Email,
                  UserName = user.UserName,
                  UserRoles = user.UserRoles.Select(L=> new RoleViewModel
                  {
                     Id  = L.Role.Id,
                     RoleName = L.Role.RoleName,
                  }).ToList(),
           }).ToList();
           if(allUsers==null) return new BaseResponse<IEnumerable<UserViewModel>>
           {
               Message = "Retrieval Unsuccessful",
               Success= false,
           };
           return new BaseResponse<IEnumerable<UserViewModel>>
           {
              Message= "Retrieval success",
               Success= true,
               Data = allUsersReturned
           };
           
        }

        public async Task<BaseResponse<UserViewModel>> LoginApplicationAdministrator(LoginAdministratorRequestModel model)
        {
            var appAdministrator = await _userRepository.GetUser(L=> L.Email == model.Email || L.UserName==model.UserName);
            if(appAdministrator == null && (BCrypt.Net.BCrypt.Verify(model.Password,appAdministrator.Password)) != true && appAdministrator.ApplicationAdministrator.AdministratorCode!= model.AdministratorCode) return new BaseResponse<UserViewModel>
            {
                Message = "Login Unsuccessful Because Of Invalid LogIn Credentials",
                Success = false,
            };
            
            return new BaseResponse<UserViewModel>
            {
                Message = "Login Success!",
                Success = true,
                Data =  new UserViewModel
                {
                  ApplicationAdministratorFullName = appAdministrator.UserName,
                   Email = appAdministrator.Email,
                  Id = appAdministrator.Id,
                  UserName = appAdministrator.UserName,
                  UserRoles = appAdministrator.UserRoles.Select(L=> new RoleViewModel
                  {
                     Id = L.Role.Id,
                     RoleName = L.Role.RoleName,
                   }).ToList(),
               },
            };
            
        }

        public async Task<BaseResponse<UserViewModel>> LoginApplicationUser(LoginUserRequestModel model)
        {
            var applicationUser = await _userRepository.GetUser(L=> L.Email == model.Email || L.UserName == model.UserName && L.IsDeleted == false);
              if(applicationUser == null  && (BCrypt.Net.BCrypt.Verify(model.Password, applicationUser.Password)) != true) return new BaseResponse<UserViewModel>
              {
                Message = " Login Failed! Invalid Log In Credentials",
                Success = false,
              };
              
              return new BaseResponse<UserViewModel>
              {
                 Message = "LogIn Succesful!",
                 Success =true,
                 Data = new UserViewModel
                 {
                   ApplicationUserFullName = applicationUser.UserName,
                   Id = applicationUser.Id,
                   Email = applicationUser.Email,
                   UserName = applicationUser.UserName,
                   UserRoles = applicationUser.UserRoles.Select( L=> new RoleViewModel
                   {
                      Id = L.Role.Id,
                      RoleName = L.Role.RoleName,
                    }).ToList()
                 }
              };

        }

       
    }
}
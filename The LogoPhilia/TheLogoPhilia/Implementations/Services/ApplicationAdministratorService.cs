using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Entities.JoinerTables;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Services
{
    public class ApplicationAdministratorService : IApplicationAdministratorService
    {

        private readonly IApplicationAdministratorRepository _ApplicationAdministratorRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ApplicationAdministratorService(IApplicationAdministratorRepository ApplicationAdministratorRepository,IUserRepository appuserR, IRoleRepository IRR,   IWebHostEnvironment webHostEnvironment)
        {
            _ApplicationAdministratorRepository = ApplicationAdministratorRepository;
            _UserRepository = appuserR;
             _roleRepository = IRR;
             _webHostEnvironment = webHostEnvironment;
        }


        public async Task<BaseResponse<ApplicationAdministratorViewModel>> CreateApplicationAdministrator(CreateApplicationAdministratorRequestModel model)
        {
           List<int> RoleIds = new List<int>();
           var roleAdmin = await _roleRepository.GetRoleByName("ApplicationAdministrator");
           int roleAdminId = roleAdmin.Id;
            RoleIds.Add(roleAdminId);
            var subAdminRole= await _roleRepository.GetRoleByName("ApplicationSubAdministrator");
             var subAdminId = subAdminRole.Id;
            RoleIds.Add(subAdminId);
          var admins = await _ApplicationAdministratorRepository.GetAllApplicationAdministrators();
          if(admins.Count() == 6) return new BaseResponse<ApplicationAdministratorViewModel>
          {
              Message = "Can No Longer Create An Administrator",
              Success = false,
          };
           var roleIds = RoleIds;
          var existingUser = _UserRepository.AlreadyExists(L => L.UserName == model.UserName); 
          var roles = await _roleRepository.GetSelectedRoles(roleIds);
          if(existingUser)  return new BaseResponse<ApplicationAdministratorViewModel>
          {
             Message ="User Creation Unsuccessful Because User Already exists",
              Success = false
          };
          var user = new User
          {
              UserName = model.UserName,
                Email = model.AdministratorEmail,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
               NormalPassword = model.Password,
          };
          foreach (var role in roles)
          {
              var userRole = new UserRole
              {
                Role = role,
                RoleId = role.Id,
                User = user,
                UserId = user.Id,
               
            };
             user.UserRoles.Add(userRole);
          }
           
           var newUser= await _UserRepository.Create(user);
       
          var ApplicationAdministrator= new ApplicationAdministrator
          {
              Age= GenerateAge(model.DateOfBirth),
              DateOfBirth = model.DateOfBirth,
              FirstName = model.FirstName,
              LastName =  model.LastName,
                User = newUser,
               AdministratorEmail = model.AdministratorEmail,
                UserId = newUser.Id,
                AdministratorCode = $"Admin{Guid.NewGuid().ToString().Substring(0,8)}TLPHILIA",
              AdministratorImage = model.AdminImage,
              AdministratorType = ApplicationEnums.AdminType.Administrator

          };
            await _ApplicationAdministratorRepository.Create(ApplicationAdministrator);
          return new BaseResponse<ApplicationAdministratorViewModel>
          {
              Message = "Registration Successful",
              Success = true,
              Data = new ApplicationAdministratorViewModel
              {
                  Age = ApplicationAdministrator.Age,
                  FullName = $"{ApplicationAdministrator.FirstName} {ApplicationAdministrator.LastName}",
                  AdministratorEmail = ApplicationAdministrator.AdministratorEmail,
                  Id = ApplicationAdministrator.Id,
                  UserId= ApplicationAdministrator.User.Id,
                  AdministratorType =  ApplicationEnums.AdminType.Administrator,
                  AdministratorCode = ApplicationAdministrator.AdministratorCode,
                  AdminImage = ApplicationAdministrator.AdministratorImage,
              }

          };
        }

         private int GenerateAge(DateTime DateOfBirth)
         {
               int age = DateTime.Now.Year - DateOfBirth.Year;
               return age;
          }
        public async Task<BaseResponse<ApplicationAdministratorViewModel>> Get(int Id)
        {
           var ApplicationAdministrator= await _ApplicationAdministratorRepository.GetApplicationAdministrator(Id);
           
           if(ApplicationAdministrator == null) return new BaseResponse<ApplicationAdministratorViewModel>
           {
               Message = " Retrieval Of App User Failed",
               Success = false,
           };
           return new BaseResponse<ApplicationAdministratorViewModel>
           {
               Message = "Retrieval Successful",
               Success = true,
               Data = new ApplicationAdministratorViewModel
               {
                 Id = ApplicationAdministrator.Id,
                  Age = ApplicationAdministrator.Age,
                  FullName = $"{ApplicationAdministrator.FirstName} {ApplicationAdministrator.LastName}",
                  DateOfBirth = ApplicationAdministrator.DateOfBirth,
                    
                     AdministratorEmail= ApplicationAdministrator.AdministratorEmail,
                     UserName= ApplicationAdministrator.User.UserName,
                      UserId = ApplicationAdministrator.UserId,
                       AdministratorType =  ApplicationEnums.AdminType.Administrator,
                       AdminImage = ApplicationAdministrator.AdministratorImage,
                       AdministratorCode = ApplicationAdministrator.AdministratorCode,
               }
           };

        }

        public async Task<BaseResponse<IEnumerable<ApplicationAdministratorViewModel>>> Get()
        {
           var ApplicationAdministrators = await _ApplicationAdministratorRepository.GetAllApplicationAdministrators();
           var ApplicationAdministratorsReturned= ApplicationAdministrators.Select( L=> new ApplicationAdministratorViewModel
           {
             Id =L.Id,
             UserName = L.User.UserName,
             AdministratorEmail= L.AdministratorEmail,
             Age = L.Age,
             UserId = L.UserId,
             FullName = $"{L.FirstName} {L.LastName}",
              AdministratorType =  ApplicationEnums.AdminType.Administrator,
              AdminImage = L.AdministratorImage,
           }).ToList();
           return new BaseResponse<IEnumerable<ApplicationAdministratorViewModel>>
           {
               Data = ApplicationAdministratorsReturned,
                 Message="Retrieval Successful",
                 Success = true,
           };
        }

        public async Task<BaseResponse<ApplicationAdministratorViewModel>> UpdateApplicationAdministrator(UpdateApplicationAdministratorRequestModel model, int Id)
        {
          var  ApplicationAdministrator=  await _ApplicationAdministratorRepository.Get(Id);
          var adminImage = "";
           if(model.AdminImage != null)
           {
              string adminPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, "AdminImage");
              Directory.CreateDirectory(adminPhotoPath);
              string adminImageType = model.AdminImage.ContentType.Split('/')[1];
              adminImage = $"Admin{Guid.NewGuid().ToString().Substring(0,9)}.{adminImageType}";
              var fullPath = Path.Combine(adminPhotoPath,adminImage);
              using (var fs = new FileStream(fullPath, FileMode.Create))
              {
                model.AdminImage.CopyTo(fs);
              }

           }
            if(ApplicationAdministrator == null)  return new BaseResponse<ApplicationAdministratorViewModel>
            {
               Message =$"Application User With Id  {Id} Not Found",
               Success = false
            };
         
             ApplicationAdministrator.FirstName =model.FirstName;
             ApplicationAdministrator.LastName= model.LastName;
            ApplicationAdministrator.DateOfBirth = model.DateOfBirth;
            ApplicationAdministrator.AdministratorImage = adminImage;
             await _ApplicationAdministratorRepository.Update(ApplicationAdministrator);

                var user = await _UserRepository.GetUser(ApplicationAdministrator.UserId);
                user.UserName = model.UserName;
                await _UserRepository.Update(user);

            return new BaseResponse<ApplicationAdministratorViewModel>
           {
               Message = $"Application User With Id  {Id} Successfully Updated",
               Success = true,
               Data = new ApplicationAdministratorViewModel
               {
                   Id = ApplicationAdministrator.Id,
                    Age = ApplicationAdministrator.Age,
                     FullName = $"{ApplicationAdministrator.FirstName} {ApplicationAdministrator.LastName}",
                      DateOfBirth = ApplicationAdministrator.DateOfBirth,
                     AdministratorEmail= ApplicationAdministrator.AdministratorEmail,
                     UserName= ApplicationAdministrator.User.UserName,
                      UserId = ApplicationAdministrator.UserId,
               },
               
           };
          
        }

        public async Task<bool> Delete(int Id)
        {
           var appUser=await _ApplicationAdministratorRepository.GetApplicationAdministrator(Id);
           if(appUser==null) return false;
             appUser.IsDeleted = true;
             _ApplicationAdministratorRepository.SaveChanges();
             return true;
        }

       

        public async Task<BaseResponse<ApplicationAdministratorViewModel>> CreateSubAdministrator(CreateSubAdministratorRequestModel model)
        {
            var roleSubAdmin = await _roleRepository.GetRoleByName("ApplicationSubAdministrator");
            var findExists = _UserRepository.AlreadyExists(L=> L.UserName == model.UserName);
            if(findExists) return new BaseResponse<ApplicationAdministratorViewModel>
            {
                 Message = "Creation Unsuccessful! Sub Administrator Already Exists",
                 Success = false,
            };
            var user = new User
            {
              UserName = model.UserName,
              Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
              Email = model.AdministratorEmail,
            
            };
            var userRole = new UserRole
            {
               
               
               Role = roleSubAdmin,
               RoleId = roleSubAdmin.Id,
               User = user,
               UserId =user.Id,
            };
            user.UserRoles.Add(userRole);
            await _UserRepository.Create(user);
            var subAdministrator = new ApplicationAdministrator
            {
                
               AdministratorEmail = model.AdministratorEmail,
               AdministratorCode =  $"SubAdmin{Guid.NewGuid().ToString().Substring(0,5)}/TLPHILIA",
               Age =GenerateAge(model.DateOfBirth),
               DateOfBirth = model.DateOfBirth,
               FirstName = model.FirstName,
               LastName = model.LastName,
               User = user,
               UserId = user.Id,
              AdministratorType = ApplicationEnums.AdminType.Administrator,
              AdministratorImage = model.AdminImage 
            };
           await _ApplicationAdministratorRepository.Create(subAdministrator);
           user.ApplicationAdministrator = subAdministrator;
           _UserRepository.SaveChanges();
           return new BaseResponse<ApplicationAdministratorViewModel>
           {
              Message= "Application Sub Administrator Initialized Successfully",
               Success = true,
               Data = new ApplicationAdministratorViewModel
               {
                  Id = subAdministrator.Id,
                  UserId = subAdministrator.UserId,
                  UserName = subAdministrator.User.UserName,
                  AdministratorCode = subAdministrator.AdministratorCode,
                  AdministratorEmail = subAdministrator.AdministratorEmail,
                  AdministratorType = ApplicationEnums.AdminType.SubAdministrator,
                  AdminImage = subAdministrator.AdministratorImage,
               }
           };
        }
    }
}
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
    public class ApplicationUserService : IApplicationUserService
    {

        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository,IUserRepository appuserR, IRoleRepository IRR, IWebHostEnvironment webHostEnvironment)
        {
            _applicationUserRepository = applicationUserRepository;
            _UserRepository = appuserR;
             _roleRepository = IRR;
             _webHostEnvironment = webHostEnvironment;
        }

        public async Task<BaseResponse<ApplicationUserViewRequestModel>> CreateApplicationUser(ApplicationUserCreateRequestModel model)
        {
              
             

           
            var role = await _roleRepository.GetRoleByName("ApplicationUser");
          var existingUser = _UserRepository.AlreadyExists(L => L.UserName == model.UserName);  
          if(existingUser)  return new BaseResponse<ApplicationUserViewRequestModel>
          {
                    Message ="User Creation Unsuccessful Because User Already exists",
                 Success = false
          };
          var user = new User
          {
              UserName = model.UserName,
                Email = model.UserEmail,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                 NormalPassword = model.Password,
          };
            var userRole = new UserRole
            {
                Role = role,
                RoleId = role.Id,
                User = user,
                UserId = user.Id,
                 
            };
            user.UserRoles.Add(userRole);
            await _UserRepository.Create(user);
          var applicationUser= new ApplicationUser
          {
              Age= GenerateAge(model.DateOfBirth),
              Country = model.Country,
              DateOfBirth = model.DateOfBirth,
              Gender = model.Gender,
              
              FirstName = model.FirstName,
              LastName =  model.LastName,
                User = user,
                UserEmail = model.UserEmail,
                UserId = user.Id,
                ApplicationUserImage = model.UserImage,
          };
           user.ApplicationUser = applicationUser;
          await _applicationUserRepository.Create(applicationUser);
          return new BaseResponse<ApplicationUserViewRequestModel>
          {
              Message = "Registration Successful",
              Success = true,
              Data = new ApplicationUserViewRequestModel{
                  Age = applicationUser.Age,
                  FullName = $"{applicationUser.FirstName} {applicationUser.LastName}",
                  UserEmail = applicationUser.UserEmail,
                  ApplicationUserId = applicationUser.Id,
                  UserId= applicationUser.User.Id,
                  UserName =applicationUser.User.UserName,
              }

          };
        }

         private int GenerateAge(DateTime DateOfBirth)
         {
               int age = DateTime.Now.Year - DateOfBirth.Year;
               return age;
          }
        public async Task<BaseResponse<ApplicationUserViewRequestModel>> Get(int Id)
        {
           var applicationUser= await _applicationUserRepository.GetUserByExpression(L=> L.UserId== Id);
           var user = await _UserRepository.GetUser(applicationUser.UserId);
           
           if(applicationUser == null) return new BaseResponse<ApplicationUserViewRequestModel>
           {
               Message = " Retrieval Of App User Failed",
               Success = false,
           };
           return new BaseResponse<ApplicationUserViewRequestModel>
           {
               Message = "Retrieval Successful",
               Success = true,
               Data = new ApplicationUserViewRequestModel
               {
                 ApplicationUserId = applicationUser.Id,
                  Age = applicationUser.Age,
                  Country = applicationUser.Country,
                  FullName = $"{applicationUser.FirstName} {applicationUser.LastName}",
                  DateOfBirth = applicationUser.DateOfBirth,
                     
                     Gender = applicationUser.Gender,
                     UserEmail= user.Email,
                     ApplicationUserImage = applicationUser.ApplicationUserImage,
                     UserName= user.UserName,
                      UserId = applicationUser.UserId,
                      ApplicationUserAdminMessages = applicationUser.ApplicationUserAdminMessages.Select(L => new ApplicationUserAdminMessageViewModel
                      {
                           AdministratorMessageId = L.AdministratorMessageId,
                          AdministratorMessage =L.AdministratorMessage.MessageContent,
                          ApplicationUserId = L.ApplicationUserId,
                          Id = L.Id,
                          ApplicationUserName = L.ApplicationUser.User.UserName,
                      }).ToList(),
                      ApplicationUserComments = applicationUser.ApplicationUserComments.Select(L=> new ApplicationUserCommentViewModel
                      {
                         ApplicationUserId = applicationUser.Id,
                         CommentContent = L.CommentContent,
                         CommentDate = L.CommentDate,
                         PostCreator= L.Post.ApplicationUser.User.UserName,
                         PostDate = L.Post.DateCreated,
                         ApplicationUserName = L.ApplicationUser.User.UserName,
                         Id = L.Id,
                      }).ToList(),
                      ApplicationUserNotes = applicationUser.ApplicationUserNotes.Select(L => new NotesViewModel
                      {
                           NoteId =L.Id,
                          ApplicationUserId = applicationUser.Id,
                           Content = L.Content,
                           DateAdded = L.DateAdded,
                           ApplicationUserUserName = L.ApplicationUser.User.UserName,
                      }).ToList(),
                      ApplicationUserPosts = applicationUser.ApplicationUserPosts.Select(L=> new ApplicationUserPostViewModel
                      {
                        PostContent =L.PostContent,
                        DatePosted = L.DatePosted,
                        PostId =L.Id,
                      }).ToList(),


               }
           };

        }

        public async Task<BaseResponse<IEnumerable<ApplicationUserViewRequestModel>>> Get()
        {
           var applicationUsers = await _applicationUserRepository.GetAllUsers();
           if(applicationUsers == null)  return new BaseResponse<IEnumerable<ApplicationUserViewRequestModel>>
            {
               Message =$"Application Users Not Found",
               Success = false
            };
          
           return new BaseResponse<IEnumerable<ApplicationUserViewRequestModel>>
           {
               Data = applicationUsers.Select( L=> new ApplicationUserViewRequestModel
              {
               ApplicationUserId =L.Id,
                UserName = L.User.UserName,
               UserEmail = L.UserEmail,
               Gender = L.Gender,
               Age = L.Age,
               Country = L.Country,
               UserId = L.UserId,
               FullName = $"{L.FirstName} {L.LastName}",
               }).ToList(),
                 Message="Retrieval Successful",
                 Success = true,
           };
        }

        public async Task<BaseResponse<ApplicationUserViewRequestModel>> UpdateApplicationUser(ApplicationUserUpdateRequestModel model, int Id)
        {
             
            var  applicationUser=  await _applicationUserRepository.GetUserByExpression(L=> L.UserId==Id);
            if(applicationUser == null)  return new BaseResponse<ApplicationUserViewRequestModel>
            {
               Message =$"Application User With Id  {Id} Not Found",
               Success = false
            };
          
                
            applicationUser.FirstName =model.FirstName;
            applicationUser.LastName= model.LastName;
            applicationUser.DateOfBirth = model.DateOfBirth;
            applicationUser.Country = model.Country;
            applicationUser.ApplicationUserImage = model.UserImage;

             await _applicationUserRepository.Update(applicationUser);

                var user = await _UserRepository.GetUser(applicationUser.UserId);
                user.UserName = model.UserName;
                await _UserRepository.Update(user);

            return new BaseResponse<ApplicationUserViewRequestModel>
           {
               Message = $"Application User With Id  {Id} Successfully Updated",
               Success = true,
               Data = new ApplicationUserViewRequestModel
               {
                   ApplicationUserId = applicationUser.Id,
                    Age = applicationUser.Age,
                     Country = applicationUser.Country,
                     FullName = $"{applicationUser.FirstName} {applicationUser.LastName}",
                      DateOfBirth = applicationUser.DateOfBirth,
                     Gender = applicationUser.Gender,
                     UserEmail= applicationUser.UserEmail,
                     ApplicationUserImage = applicationUser.ApplicationUserImage,
                     UserName= applicationUser.User.UserName,
                      UserId = applicationUser.UserId,
               },
               
           };
          
        }

        public async Task<bool> Delete(int Id)
        {
           var appUser=await _applicationUserRepository.GetUser(Id);
           if(appUser==null) return false;
             appUser.IsDeleted = true;
             _applicationUserRepository.SaveChanges();
             return true;
        }
    }
}
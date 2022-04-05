using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Entities.JoinerTables;
using TheLogoPhilia.Interfaces;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Services
{
    public class AdministratorMessageService : IAdministratorMessageService
    {
        private readonly IAdministratorMessageRepository _administratorMessageRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IMessageSender _messageSender;
        private readonly IMessageRepository _messageRepository;
        private readonly IPostLogRepository _postLogRepository;
        private readonly IApplicationUserAdminMessageRepository _appUserAdminMailRepository;

        public AdministratorMessageService(IAdministratorMessageRepository administratorMessageRepository, IApplicationUserRepository applicationUserRepository,
         IMessageSender messageSender, IMessageRepository messageRepository, IPostLogRepository postLogRepository, IApplicationUserAdminMessageRepository appUserAdminMailRepository)
        {
            _administratorMessageRepository = administratorMessageRepository;
            _applicationUserRepository = applicationUserRepository;
            _messageSender = messageSender;
            _messageRepository = messageRepository;
            _postLogRepository = postLogRepository;
             _appUserAdminMailRepository =  appUserAdminMailRepository;
        }

        public async  Task<BaseResponse<AdministratorMessageViewModel>> CreateAdministratorMessageForDiscussionToMany(CreateAdministratorMessageRequestModel model)
        {
           var messageInDb = await _messageRepository.GetMessageByType(model.MessageType);
            string messageReturned = messageInDb.MessageContent;
            var users = await _applicationUserRepository.GetAllUsers();
            List<int> UserIds = users.Select(L=> L.Id).ToList();
            var usersEmails = users.Select(L=> L.UserEmail).ToList();
            var postLogs = await _postLogRepository.GetLogsOfPostCreatedToday();
            var postUrls = postLogs.Select(L=> L.PostUrl).ToList();
            var selectedUsers = await _applicationUserRepository.GetSelectedApplicationUsers(UserIds);
            if(messageReturned == null) return new BaseResponse<AdministratorMessageViewModel>
            {
               Message = "Message Not Created",
               Success = false,
            };
            
             var adminMessage = new AdministratorMessage
            {
              DateSent = DateTime.UtcNow,
              MessageContent = messageReturned,
             MessageSubject = model.MessageSubject,
              MessageType = model.MessageType, 
            };
             var sentStatus = await _messageSender.SendMailToMultipleUserAboutDiscussion(model.MessageSubject, usersEmails,messageReturned,postUrls);
            
               var message=  await _administratorMessageRepository.Create(adminMessage);
            
            foreach(var user in selectedUsers)
            {
               var appAdminMessage = new ApplicationUserAdminMessage
               {
                  ApplicationUserId = user.Id,
                  ApplicationUser = user,
                  AdministratorMessage = message,
                  AdministratorMessageId = message.Id,
               };
               adminMessage.ApplicationUserAdminMessages.Add(appAdminMessage);
            }
           
           
            return new BaseResponse<AdministratorMessageViewModel>
            {
                     Message =sentStatus,
                     Success = true,
                     Data = new AdministratorMessageViewModel
                     {
                         Id = adminMessage.Id,
                         DateSent = message.DateSent,
                         MessageContent = message.MessageContent,
                         MessageSubject = message.MessageSubject,
                         MessageType = message.MessageType,
                     }
            };
        }

        public async Task<BaseResponse<AdministratorMessageViewModel>> CreateAdministratorMessageToBirthdayUsers(CreateAdministratorMessageRequestModel model)
        {
            var messageInDb = await _messageRepository.GetMessageByType(ApplicationEnums.MessageType.BirthdayGreetings);
            string messageReturned = messageInDb.MessageContent;
            var users = await _applicationUserRepository.GetBirthDayUsers();
            var usersEmails = users.Select(L=> L.UserEmail).ToList();
            List<int> UserIds = users.Select(L=> L.Id).ToList();
             var selectedUsers = await _applicationUserRepository.GetSelectedApplicationUsers(UserIds);
              if(messageReturned == null) return new BaseResponse<AdministratorMessageViewModel>
            {
               Message = "Message Not Created",
               Success = false,
            };
             var adminMessage = new AdministratorMessage
                   {

                      DateSent = DateTime.UtcNow,
                      MessageContent = messageReturned,
                      MessageSubject = model.MessageSubject,
                      MessageType = ApplicationEnums.MessageType.BirthdayGreetings, 
                   };
            foreach(var user in selectedUsers)
            {
               var appAdminMessage = new ApplicationUserAdminMessage
               {
                  ApplicationUserId = user.Id,
                  ApplicationUser = user,
                  AdministratorMessage = adminMessage,
                  AdministratorMessageId = adminMessage.Id,
               };
               adminMessage.ApplicationUserAdminMessages.Add(appAdminMessage);
            }
            var sentStatus =  _messageSender.SendMailToMultipleUser(model.MessageSubject, usersEmails,messageReturned);
            await _administratorMessageRepository.Create(adminMessage);
            return new BaseResponse<AdministratorMessageViewModel>
            {
                     Message =sentStatus,
                     Success = true,
                     Data = new AdministratorMessageViewModel
                     {
                         Id = adminMessage.Id,
                         DateSent = adminMessage.DateSent,
                         MessageContent = adminMessage.MessageContent,
                         MessageSubject = adminMessage.MessageSubject,
                         MessageType = adminMessage.MessageType,
                     }
            };
        }

        public async Task<BaseResponse<AdministratorMessageViewModel>> CreateAdministratorMessageToManyUsers(CreateAdministratorMessageRequestModel model)
        {
           var messageInDb = await _messageRepository.GetMessageByType(model.MessageType);
            string messageReturned = messageInDb.MessageContent;
            var users = await _applicationUserRepository.GetAllUsers();
            List<int> UserIds = users.Select(L=> L.Id).ToList();
            var usersEmails = users.Select(L=> L.UserEmail).ToList();
            var selectedUsers = await _applicationUserRepository.GetSelectedApplicationUsers(UserIds);
            if(messageReturned == null) return new BaseResponse<AdministratorMessageViewModel>
            {
               Message = "Message Not Created",
               Success = false,
            };
            var adminMessage = new AdministratorMessage
            {
                    
                      DateSent = DateTime.UtcNow,
                      MessageContent = messageReturned,
                      MessageSubject = model.MessageSubject,
                      MessageType = model.MessageType, 
            };
            foreach(var user in selectedUsers)
            {
               var appAdminMessage = new ApplicationUserAdminMessage
               {
                  ApplicationUserId = user.Id,
                  ApplicationUser = user,
                  AdministratorMessage = adminMessage,
                  AdministratorMessageId = adminMessage.Id,
               };
               adminMessage.ApplicationUserAdminMessages.Add(appAdminMessage);
            }
            var sentStatus =  _messageSender.SendMailToMultipleUser(model.MessageSubject, usersEmails,messageReturned);
            await _administratorMessageRepository.Create(adminMessage);
            return new BaseResponse<AdministratorMessageViewModel>
            {
                     Message =sentStatus,
                     Success = true,
                     Data = new AdministratorMessageViewModel
                     {
                         Id = adminMessage.Id,
                         DateSent = adminMessage.DateSent,
                         MessageContent = adminMessage.MessageContent,
                         MessageSubject = adminMessage.MessageSubject,
                         MessageType = adminMessage.MessageType,
                     }
            };

        }

        public async Task<BaseResponse<AdministratorMessageViewModel>> CreateAdministratorMessageToUser(CreateAdministratorMessageRequestModel model, int UserId)
        {
             var messageInDb = await _messageRepository.GetMessageByType(model.MessageType);
            string messageReturned = messageInDb.MessageContent;
            var user = await _applicationUserRepository.GetUser(UserId);
            var userEmail = user.UserEmail;
            if(messageReturned== null) return new BaseResponse<AdministratorMessageViewModel>
            {
               Message ="Message Not Sent",
               Success =false,
            };
            var adminMessage = new AdministratorMessage
            {
               
                DateSent = DateTime.UtcNow,
                      MessageContent = messageReturned,
                      MessageSubject = model.MessageSubject,
                      MessageType = model.MessageType, 
            };
            
            var appUserAdminMessage = new ApplicationUserAdminMessage
            {
               AdministratorMessage = adminMessage,
               AdministratorMessageId = adminMessage.Id,
               ApplicationUser = user,
               ApplicationUserId = user.Id,
             
            };
            adminMessage.ApplicationUserAdminMessages.Add(appUserAdminMessage);
            var sentStatus = _messageSender.SendMailToSingleUser(model.MessageSubject, userEmail,messageReturned);
            await _administratorMessageRepository.Create(adminMessage);
            return new BaseResponse<AdministratorMessageViewModel>
            {
              Message= sentStatus,
              Success = true,
              Data = new AdministratorMessageViewModel
              {
                  Id = adminMessage.Id,
                DateSent = adminMessage.DateSent,
                MessageContent = adminMessage.MessageContent,
                MessageSubject = adminMessage.MessageSubject,
                MessageType = adminMessage.MessageType,
              }
            };
        }

        public async Task<bool> Delete(int Id)
        {
            var adminMessage = await _administratorMessageRepository.GetAdminMessage(Id);
            if(adminMessage==null) return false;
             adminMessage.IsDeleted = true;
             _administratorMessageRepository.SaveChanges();
             return true;
        }

        public async Task<BaseResponse<AdministratorMessageViewModel>> Get(int Id)
        {
            var adminMessage = await _administratorMessageRepository.GetAdminMessage(Id);
            if(adminMessage==null) return new BaseResponse<AdministratorMessageViewModel>
            {
                Message = $"Could Not Get Message With Id {Id}",
                Success = false,
            };
            return new BaseResponse<AdministratorMessageViewModel>
            {
              Message = "Successfully Retrieved ",
              Success = true,
              Data = new AdministratorMessageViewModel
              {
                  Id = adminMessage.Id,
                   DateSent = adminMessage.DateSent,
                MessageContent = adminMessage.MessageContent,
                MessageSubject = adminMessage.MessageSubject,
                MessageType = adminMessage.MessageType,
                ApplicationUserAdminMessages = adminMessage.ApplicationUserAdminMessages.Select(adminMessage=> new ApplicationUserAdminMessageViewModel
                {
                    Id = adminMessage.Id,
                    AdministratorMessage = adminMessage.AdministratorMessage.MessageContent,
                    AdministratorMessageId = adminMessage.AdministratorMessageId,
                    ApplicationUserId = adminMessage.ApplicationUserId,
                    ApplicationUserName = adminMessage.ApplicationUser.User.UserName,

                }).ToList()
              }
            };
        }

        public async Task<BaseResponse<IEnumerable<AdministratorMessageViewModel>>> Get()
        {
           var adminMessage = await _administratorMessageRepository.GetAdminMessages();
           var adminMessagesReturned = adminMessage.Select(administratorMessage=> new AdministratorMessageViewModel
           {
               Id = administratorMessage.Id,
               DateSent = administratorMessage.DateSent,
               MessageContent = administratorMessage.MessageContent,
               MessageSubject = administratorMessage.MessageSubject,
               MessageType = administratorMessage.MessageType,
               ApplicationUserAdminMessages = administratorMessage.ApplicationUserAdminMessages.Select(L=> new ApplicationUserAdminMessageViewModel
               {  
                   Id = L.Id,
                  AdministratorMessage  = L.AdministratorMessage.MessageContent,
                  AdministratorMessageId = L.AdministratorMessageId,
                  ApplicationUserId = L.ApplicationUserId,
                  ApplicationUserName = L.ApplicationUser.User.UserName,
               }).ToList()
           }).ToList();
           return new BaseResponse<IEnumerable<AdministratorMessageViewModel>>
           {
               Message = "Retrieval Successful",
               Success = true,
               Data = adminMessagesReturned,
           };
        }

       

        public async Task<BaseResponse<AdministratorMessageViewModel>> UpdateAdministratorMessage(UpdateAdministratorMessageRequestModel model,int Id)
        {
           var adminMessage = await _administratorMessageRepository.GetAdminMessage(Id);
           if(adminMessage == null) return new BaseResponse<AdministratorMessageViewModel>
           {
               Message = "Update Failed, due to retrieval error",
               Success = false,
           };
           adminMessage.DateSent = model.DateSent;
          adminMessage.MessageSubject = adminMessage.MessageSubject ?? model.MessageSubject;
          adminMessage.MessageType = adminMessage.MessageType;
           await _administratorMessageRepository.Update(adminMessage);
           return new BaseResponse<AdministratorMessageViewModel>
           {
               Message = "Update Successful",
               Success = true,
               Data = new AdministratorMessageViewModel
               {
                   Id = adminMessage.Id,
                       DateSent = adminMessage.DateSent,
                    MessageContent = adminMessage.MessageContent,
                   MessageSubject = adminMessage.MessageSubject,
                     MessageType = adminMessage.MessageType,  
                     ApplicationUserAdminMessages = adminMessage.ApplicationUserAdminMessages.Select( L=> new ApplicationUserAdminMessageViewModel
                     {
                             Id = L.Id,
                          AdministratorMessage  = L.AdministratorMessage.MessageContent,
                           AdministratorMessageId = L.AdministratorMessageId,
                            ApplicationUserId = L.ApplicationUserId,
                            ApplicationUserName = L.ApplicationUser.User.UserName,
                     }).ToList()
               }
           };
        }

       public async Task<BaseResponse<IEnumerable<ApplicationUserAdminMessageViewModel>>> GetAdminMessagesToAUser(int UserId)
        {
           var administratorMessageToUser = await _appUserAdminMailRepository.GetApplicationUserAdminMessageToUser(UserId);
           var adminMessagesToUserReturned = administratorMessageToUser.Select(L=> new ApplicationUserAdminMessageViewModel
           {
                  AdministratorMessage = L.AdministratorMessage.MessageContent,
                  AdministratorMessageId =L.AdministratorMessageId,
                  ApplicationUserId =  L.ApplicationUserId,
                  ApplicationUserName = L.ApplicationUser.User.UserName,
           }).ToList();
           return new BaseResponse<IEnumerable<ApplicationUserAdminMessageViewModel>>
           {
               Message = "Retrieval  Successful",
               Success =true,
               Data = adminMessagesToUserReturned,
           };
        }
    }
}
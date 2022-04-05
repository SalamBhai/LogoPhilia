using System;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Entities;
using System.Linq;

namespace TheLogoPhilia.Implementations.Services
{
    public class MessageService : IMessageService
    { 
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<BaseResponse<MessageViewModel>> Create(CreateMessageRequestModel model)
        {
            var message = new Message
            {
               MessageType = model.MessageType,
                MessageContent = model.MessageContent,
                 
            };
             await _messageRepository.Create(message);
            return new BaseResponse<MessageViewModel>
            {
               Message = "Creation Successful",
               Success = true,
               Data = new MessageViewModel
               {
                    Id = message.Id,
                    MessageType= message.MessageType,
                    MessageContent= message.MessageContent,
                    MessageSubject = message.MessageType.ToString(),

               }
            };
           
        }
        public async Task<BaseResponse<MessageViewModel>> Get(int Id)
        {
            var message = await _messageRepository.Get(Id);
            if(message == null) return new BaseResponse<MessageViewModel> 
            {
                Message = $"Message With Id {Id} Not Found ",
                Success = false,
            };
            return new BaseResponse<MessageViewModel>
            {
                Message = " Retrieval Success",
                Success = true,
                Data = new MessageViewModel{
                  Id = message.Id,
                  MessageContent = message.MessageContent,
                  MessageType = message.MessageType  
                }
            };
        }
        public async Task<BaseResponse<MessageViewModel>> Update(UpdateMessageRequestModel model,  int Id)
        {
            var message = await _messageRepository.Get(Id);
            if(message == null) return new BaseResponse<MessageViewModel> 
            {
                Message = $"Message With Id {Id} Not Found ",
                Success = false,
            };
              message.MessageType = model.MessageType;
              message.MessageContent = model.MessageContent;
              await _messageRepository.Update(message);
            return new BaseResponse<MessageViewModel> 
            {
                Message = $"Message With Id {Id} Found ",
                Success = true,
                Data = new MessageViewModel
                {
                    Id = message.Id,
                    MessageType = message.MessageType,
                    MessageContent= message.MessageContent,
                }
            };
        }
        public async Task<BaseResponse<MessageViewModel>> GetMessageByType(MessageType messageType)
        {
            var message = await _messageRepository.GetMessageByType(messageType);
            if(message == null) return new BaseResponse<MessageViewModel>
            {
                Message = $"Message With Type {messageType} Not Found ",
                Success = false,
            };
            return new BaseResponse<MessageViewModel> 
            {
                Message = $"Message With Type {messageType} Found ",
                Success = true,
                Data = new MessageViewModel
                {
                    Id = message.Id,
                    MessageType = message.MessageType,
                    MessageContent= message.MessageContent,
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<MessageViewModel>>> Get()
        {
            var messages = await _messageRepository.Get();
            var messagesReturned = messages.Select(L => new MessageViewModel
            {
                  Id = L.Id,
                  MessageContent = L.MessageContent,
                  MessageType = L.MessageType,
                  MessageSubject = L.MessageSubject
            }).ToList();
            return new BaseResponse<IEnumerable<MessageViewModel>>
            {
                  Success = true,
                   Message = "Retrieval Successful",
                   Data= messagesReturned
            };
        }

        public async Task<bool> Delete( int Id)
        {
           var message = await _messageRepository.Get(Id);
           if(message==null) return false;
            message.IsDeleted =true;
            _messageRepository.SaveChanges();
            return true;
        }
    }
}
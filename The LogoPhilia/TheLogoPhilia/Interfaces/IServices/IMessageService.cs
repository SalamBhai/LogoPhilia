using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;
using TheLogoPhilia.ApplicationEnums;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
namespace TheLogoPhilia.Interfaces.IServices
{
    public interface IMessageService
    {
        Task<BaseResponse<MessageViewModel>> Create(CreateMessageRequestModel model);
        Task<BaseResponse<MessageViewModel>> Update(UpdateMessageRequestModel model,int Id);
        Task<BaseResponse<MessageViewModel>> Get(int Id);
        Task<BaseResponse<IEnumerable<MessageViewModel>>> Get();
        Task<BaseResponse<MessageViewModel>> GetMessageByType(MessageType MessageType);
       Task<bool> Delete(int Id);
    }
}
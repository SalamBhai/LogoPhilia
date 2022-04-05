using System.Threading.Tasks;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Entities;

namespace TheLogoPhilia.Interfaces.IRepositories
{
    public interface IMessageRepository : IRepository<Message>
    {
         Task<Message> GetMessageByType(MessageType messageType);
    }
}
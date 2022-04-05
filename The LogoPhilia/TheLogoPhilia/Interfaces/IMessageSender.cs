using System.Collections.Generic;
using System.Threading.Tasks;
using TheLogoPhilia.Models;
namespace TheLogoPhilia.Interfaces
{
    public interface IMessageSender
    {
        Task<string> SendMailToMultipleUserAboutDiscussion(string Subject,  List<string> userEmails, string message, List<string>PostUrls);
        string SendMailToMultipleUser( string Subject,List<string> userEmails, string message);
        string SendMailToSingleUser( string Subject, string userEmail, string message);
    }
}
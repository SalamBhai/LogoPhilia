using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Interfaces.IServices
{
    public interface IPostLogService
    {
         Task<BaseResponse<PostLogViewModel>> CreatePostLog(CreatePostLogRequestModel model);
    }
}
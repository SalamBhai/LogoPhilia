using System.Threading.Tasks;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;
using System;
namespace TheLogoPhilia.Implementations.Services
{
    public class PostLogService : IPostLogService
    {
        private readonly IPostLogRepository _postLogRepository;

        public PostLogService(IPostLogRepository postLogRepository)
        {
            _postLogRepository = postLogRepository;
        }

        public async Task<BaseResponse<PostLogViewModel>> CreatePostLog(CreatePostLogRequestModel model)
        {
            var postLog = new PostLog
            {
                ApplicationUserPostId= model.PostId,
                PostUrl= model.PostUrl,
                DateCreated = DateTime.UtcNow
            };
            await _postLogRepository.Create(postLog);
            return new BaseResponse<PostLogViewModel>
            {
              Message = "Created post Log Successfully",
             Success=true,
             Data= new PostLogViewModel
               {
                 ApplicationUserPostUserName= postLog.ApplicationUserPost.ApplicationUser.User.UserName,
                 Id= postLog.Id,
                 PostId= postLog.ApplicationUserPost.Id,
                 PostUrl =postLog.PostUrl
                } 
            };
        }
    }
}
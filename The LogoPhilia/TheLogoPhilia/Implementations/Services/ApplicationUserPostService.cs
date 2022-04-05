using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Implementations.Services
{
    public class ApplicationUserPostService : IApplicationUserPostService
    {
        private readonly IApplicationUserPostRepository _appUserPostRepository;
        private readonly IApplicationUserRepository _appUserRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ApplicationUserPostService(IApplicationUserPostRepository appUserPostRepository, IApplicationUserRepository appUserRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _appUserPostRepository = appUserPostRepository;
            _appUserRepository = appUserRepository;
            _UserRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<BaseResponse<ApplicationUserPostViewModel>> Create(CreateApplicationUserPostViewModel model, int UserId)
        {
            var userInPost = await _UserRepository.GetUser(UserId);
                var videoFile = "";
           if(model.VideoFile != null)
            {
              string wordAudioPath = Path.Combine(_webHostEnvironment.WebRootPath, "AdminImage");
               Directory.CreateDirectory(wordAudioPath);
               string videoFileType = model.VideoFile.ContentType.Split('/')[1];
               videoFile = $"Post/{Guid.NewGuid().ToString().Substring(0,9)}.{videoFileType}";
                var fullPath = Path.Combine(wordAudioPath,videoFile);
                if(videoFileType.ToLower() != "mp4" )
                {
                  throw new Exception("File Type Not Supported");
                }
               
                else
                {
                    using (var fs = new FileStream(fullPath, FileMode.Create))
                     {
                     model.VideoFile.CopyTo(fs);
                   }
                }  
            }
           var post = new ApplicationUserPost 
           {
               PostContent = model.PostContent,
               DatePosted = DateTime.UtcNow,
               ApplicationUserId = userInPost.ApplicationUser.Id,
               ApplicationUser = userInPost.ApplicationUser,
               VideoFile = videoFile,
                
           };
           await _appUserPostRepository.Create(post);
           return new BaseResponse<ApplicationUserPostViewModel>
           {
               Message = "Post Created Successfully",
               Success = true,
               Data = new ApplicationUserPostViewModel
               {
                   ApplicationUserName = post.ApplicationUser.User.UserName,
                   PostContent = post.PostContent,
                   VideoFile = post.VideoFile,
                   DatePosted = post.DatePosted,
                   ApplicationUserId = post.ApplicationUserId,
                   ApplicationUserEmail = post.ApplicationUser.UserEmail,
                   PostId = post.Id,
               }
           };
        }

        

        public async Task<BaseResponse<ApplicationUserPostViewModel>> Get(int Id)
        {
            var appUserPost = await _appUserPostRepository.GetApplicationUserPost(Id);
            var appUserInPost = await  _appUserRepository.GetUser(appUserPost.ApplicationUserId);
            var appUser= await _UserRepository.GetUser(appUserInPost.UserId);
            var appUserPostReturned =  new ApplicationUserPostViewModel
            {
                ApplicationUserId = appUserPost.ApplicationUserId,
                PostId = appUserPost.Id,
                ApplicationUserName = appUser.UserName,
                ApplicationUserEmail = appUserPost.ApplicationUser.UserEmail,
                PostContent = appUserPost.PostContent,
                VideoFile = appUserPost.VideoFile,
                DatePosted = appUserPost.DatePosted,

                ApplicationUserComments = appUserPost.ApplicationUserComments.Select( L=> new ApplicationUserCommentViewModel
                 {
                   ApplicationUserId = L.ApplicationUserId,
                   ApplicationUserName = L.ApplicationUser.User.UserName,
                   CommentContent = L.CommentContent,
                   PostId = L.PostId,
                   CommentDate = L.CommentDate,
                   PostCreator = L.Post.ApplicationUser.User.UserName,
                   PostDate =L.Post.DateCreated,
                   Id = L.Id,
                  }
                ).ToList(),

            };
            return new BaseResponse<ApplicationUserPostViewModel>
           {
                Message = "Post Retrieval Successful",
                Success = true,
                Data= appUserPostReturned,
           };
        }

        public async Task<BaseResponse<IEnumerable<ApplicationUserPostViewModel>>> Get()
        {
           var userPost = await _appUserPostRepository.GetAllPosts();
           var userPostsReturned = userPost.Select(appUserPost=> new ApplicationUserPostViewModel
           {
               ApplicationUserId = appUserPost.ApplicationUserId,
                PostId = appUserPost.Id,
                ApplicationUserName = appUserPost.ApplicationUser.User.UserName,
                ApplicationUserEmail = appUserPost.ApplicationUser.UserEmail,
                PostContent = appUserPost.PostContent,
                VideoFile = appUserPost.VideoFile,
                DatePosted = appUserPost.DatePosted,
                
                ApplicationUserComments = appUserPost.ApplicationUserComments.Select( L=> new ApplicationUserCommentViewModel
                 {
                   ApplicationUserId = L.ApplicationUserId,
                   ApplicationUserName = L.ApplicationUser.User.UserName,
                   CommentContent = L.CommentContent,
                   PostId = L.PostId,
                   CommentDate = L.CommentDate,
                   PostCreator = L.Post.ApplicationUser.User.UserName,
                   PostDate =L.Post.DateCreated,
                   Id = L.Id,
                  }
                ).ToList(),   
           }).ToList();
           return new BaseResponse<IEnumerable<ApplicationUserPostViewModel>>
           {
                Message = "User Retrieval Successful",
                Success = true,
                Data= userPostsReturned,
           };
        }

        public async Task<BaseResponse<ApplicationUserPostViewModel>> Update(UpdateApplicationUserPostViewModel model, int Id)
        {
          var appUserPost= await _appUserPostRepository.GetApplicationUserPost(Id);
          if(appUserPost == null) return new BaseResponse<ApplicationUserPostViewModel>
          {
           Message = "Post Not Found",
           Success = false,
          };
          appUserPost.PostContent = model.PostContent;
          await _appUserPostRepository.Update(appUserPost);
           return new BaseResponse<ApplicationUserPostViewModel>
           {
               Message = "Update Successful",
               Success =true,
               Data = new ApplicationUserPostViewModel
               {
                    ApplicationUserId = appUserPost.ApplicationUserId,
                    PostId = appUserPost.Id,
                    ApplicationUserName = appUserPost.ApplicationUser.User.UserName,
                    ApplicationUserEmail = appUserPost.ApplicationUser.UserEmail,
                   PostContent = appUserPost.PostContent,
                   VideoFile = appUserPost.VideoFile,
                    DatePosted = appUserPost.DatePosted,
                
                  ApplicationUserComments = appUserPost.ApplicationUserComments.Select( L=> new ApplicationUserCommentViewModel
                 {
                   ApplicationUserId = L.ApplicationUserId,
                   ApplicationUserName = L.ApplicationUser.User.UserName,
                   CommentContent = L.CommentContent,
                   PostId = L.PostId,
                   CommentDate = L.CommentDate,
                   PostCreator = L.Post.ApplicationUser.User.UserName,
                   PostDate =L.Post.DateCreated,
                   Id = L.Id,
                  }
                ).ToList(),   
               }
           };
        }

        public async Task<bool> Delete(int Id)
        {
           var post = await _appUserPostRepository.GetApplicationUserPost(Id);
           if(post==null) return false;
           post.IsDeleted = true;
           _appUserPostRepository.SaveChanges();
           return true;
        }

        public async Task<BaseResponse<IEnumerable<ApplicationUserPostViewModel>>> GetPostsOfUser(int UserId)
        {         
            var userinPost = await _UserRepository.GetUser(UserId);
            var userId = userinPost.ApplicationUser.Id;
             var postsOfUser = await _appUserPostRepository.GetAllPosts(L=> L.ApplicationUserId == userId);
             if(postsOfUser == null) return new BaseResponse<IEnumerable<ApplicationUserPostViewModel>>
          {
           Message = "Post Not Found",
           Success = false,
          };
        
          
           return new  BaseResponse<IEnumerable<ApplicationUserPostViewModel>>
           {
               Message = "User post found Successfully",
               Success =true,
               Data =  postsOfUser.Select(postOfUser=> new ApplicationUserPostViewModel
               {
                   ApplicationUserId = postOfUser.ApplicationUserId,
                    PostId = postOfUser.Id,
                    ApplicationUserName = postOfUser.ApplicationUser.User.UserName,
                    ApplicationUserEmail = postOfUser.ApplicationUser.UserEmail,
                   PostContent = postOfUser.PostContent,
                   VideoFile = postOfUser.VideoFile,
                    DatePosted = postOfUser.DatePosted,
                
                  ApplicationUserComments = postOfUser.ApplicationUserComments.Select( L=> new ApplicationUserCommentViewModel
                 {
                   ApplicationUserId = L.ApplicationUserId,
                   ApplicationUserName = L.ApplicationUser.User.UserName,
                   CommentContent = L.CommentContent,
                   PostId = L.PostId,
                   CommentDate = L.CommentDate,
                   PostCreator = L.Post.ApplicationUser.User.UserName,
                   PostDate =L.Post.DateCreated,
                   Id = L.Id,
                  }
                ).ToList(),   
               }).ToList()
           };
        }
    }
}
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IRepositories;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationUserPostController : ControllerBase
    {
        private readonly IApplicationUserPostService _applicationUserPostService;
         private readonly IPostLogRepository _postLogRepository;

        public ApplicationUserPostController(IApplicationUserPostService applicationUserPostService, IPostLogRepository postLogRepository)
        {
            _applicationUserPostService = applicationUserPostService;
            _postLogRepository = postLogRepository;
        }

        [HttpPost("CreatePost")]
        [Authorize(Roles = "ApplicationUser")]
        public async Task<IActionResult> CreatePost(CreateApplicationUserPostViewModel model)
        {
            var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           var post = await _applicationUserPostService.Create(model,UserId);
           if(!post.Success) return BadRequest();
           if(post.Success)
           {
              
               var postLog = new PostLog
               {
                   PostUrl = $"https://localhost:5001/api/ApplicationUserPost/GetPost/{post.Data.PostId}",
                   ApplicationUserPostId = post.Data.PostId,
               };
               
               await _postLogRepository.Create(postLog);
           }
           return Ok(post);
        }
        [HttpGet("GetPost/{Id}")]
        // [Authorize(Roles = "ApplicationUser")]
        public async Task<IActionResult> GetPost(int Id)
        {
           var post = await _applicationUserPostService.Get(Id);
           if(!post.Success) return BadRequest();
           return Ok(post);
        }
        [HttpGet("GetAllPosts")]
        // [Authorize(Roles = "ApplicationUser")]
        public async Task<IActionResult> GetAllPosts()
        {
           var post = await _applicationUserPostService.Get();
           if(!post.Success) return BadRequest();
           return Ok(post);
        }
        [HttpPut("UpdatePost/{Id}")]
        // [Authorize(Roles = "ApplicationUser")]
        public async Task<IActionResult> UpdatePost(UpdateApplicationUserPostViewModel model, int id)
        {
           var post = await _applicationUserPostService.Update(model,id);
           if(!post.Success) return BadRequest();
           return Ok(post);
        }
        [HttpGet("GetPostOfLoggedInUser")]
        [Authorize(Roles = "ApplicationUser")]
        public async Task<IActionResult> GetPostOfLoggedInUser()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           var post = await _applicationUserPostService.GetPostsOfUser(id);
           if(!post.Success) return BadRequest();
           return Ok(post);
        }
    }
}
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webhostEnvironment;

        public ApplicationUserPostController(IApplicationUserPostService applicationUserPostService, IWebHostEnvironment whe)
        {
            _applicationUserPostService = applicationUserPostService;
            _webhostEnvironment= whe;
            
        }

        [HttpPost("CreatePost")]
        [Authorize(Roles = "ApplicationUser")]
        public async Task<IActionResult> CreatePost([FromForm] CreateApplicationUserPostViewModel model)
        {
            var files = HttpContext.Request.Form;
                if(files.Count!=0)
                {
                    string VideoDirectory = Path.Combine(_webhostEnvironment.WebRootPath,"VideoPostsFiles");
                     Directory.CreateDirectory(VideoDirectory);
                     foreach (var file in files.Files)
                     {
                          FileInfo fileInfo= new FileInfo(file.FileName);
                          string videoFile = "post" +  Guid.NewGuid().ToString().Substring(0,7) + $"{fileInfo.Extension}";
                          string fullPath= Path.Combine(VideoDirectory,videoFile);
                          using(var fileStream= new FileStream(fullPath,FileMode.Create))
                          {
                              file.CopyTo(fileStream);
                          }
                          model.VideoFile = videoFile;
                     }
                }
            var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           var post = await _applicationUserPostService.Create(model,UserId);
           if(!post.Success) return BadRequest();
           
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
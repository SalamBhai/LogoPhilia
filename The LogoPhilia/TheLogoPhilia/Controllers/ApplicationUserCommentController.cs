using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class ApplicationUserCommentController : ControllerBase
    {
        private readonly IApplicationUserCommentService _appCommentService;

        public ApplicationUserCommentController(IApplicationUserCommentService appCommentService)
        {
            _appCommentService = appCommentService;
        }
        [Authorize(Roles ="ApplicationUser")]
        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment(CreateApplicationUserCommentModel model)
        {
            int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var comment = await _appCommentService.CreateComment(model,UserId);
            if(!comment.Success) return BadRequest(comment);
            return Ok(comment);
        }
        [HttpGet("GetCommentsOfAPost/{PostId}")]
        public async Task<IActionResult> GetCommentsOfAPost([FromRoute]  int PostId)
        {

            var comment = await _appCommentService.GetCommentsOfAPost(PostId);
            if(!comment.Success) return BadRequest(comment);
            return Ok(comment);
        }
        [HttpPut("UpdateAComment/{Id}")]
        public async Task<IActionResult> UpdateComment(UpdateApplicationUserCommentModel model,  int CommentId)
        {

            var comment = await _appCommentService.UpdateComment(model, CommentId);
            if(!comment.Success) return BadRequest(comment);
            return Ok(comment);
        }
         [HttpGet("GetApplicationComments")]
        public async Task<IActionResult> GetComments()
        {
            var comment = await _appCommentService.Get();
            if(!comment.Success) return BadRequest(comment);
            return Ok(comment);
        }
         [HttpGet("GetAnApplicationComment/{CommentId}")]
        public async Task<IActionResult> GetAComment([FromRoute]  int CommentId)
        {
            var comment = await _appCommentService.Get(CommentId);
            if(!comment.Success) return BadRequest(comment);
            return Ok(comment);
        }
    }
}
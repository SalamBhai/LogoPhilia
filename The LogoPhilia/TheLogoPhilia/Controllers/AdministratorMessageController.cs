using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.ApplicationEnums;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AdministratorMessageController : ControllerBase
    {
         private readonly IAdministratorMessageService _administratorMessageService;

        public AdministratorMessageController(IAdministratorMessageService administratorMessageService)
        {
            _administratorMessageService = administratorMessageService;
        }
       
        [HttpPost("CreateAdministratorMailToManyUsersOnDiscussion")]
         [Authorize(Roles="ApplicationAdministrator")]
         [Authorize(Roles="ApplicationSubAdministrator")]
        public async Task<IActionResult> CreateAdministratorMailForDiscussionToManyUsers([FromBody] CreateAdministratorMessageRequestModel model)
        {
            var mailsToUser = await _administratorMessageService.CreateAdministratorMessageForDiscussionToMany(model);
            if(!mailsToUser.Success) return BadRequest(mailsToUser);
            return Ok(mailsToUser);
        }
        [Authorize(Roles="ApplicationAdministrator")]
         [Authorize(Roles="ApplicationSubAdministrator")]
        [HttpPost("CreateAdministratorMailToBirthdayUsers")]
        public async Task<IActionResult> CreateAdministratorMailToBirthdayUsers([FromBody] CreateAdministratorMessageRequestModel model)
        {
            var mailsToUser = await _administratorMessageService.CreateAdministratorMessageToBirthdayUsers(model);
            if(!mailsToUser.Success) return BadRequest(mailsToUser);
             return Ok(mailsToUser);
        }
        //    [Authorize(Roles="ApplicationAdministrator")]
        //   [Authorize(Roles="ApplicationSubAdministrator")]
        [HttpPost("CreateAdministratorMailToManyUsers")]
        public async Task<IActionResult> CreateAdministratorMailToUsers([FromBody] CreateAdministratorMessageRequestModel model)
        {
            var mailsToUser = await _administratorMessageService.CreateAdministratorMessageToManyUsers(model);
            if(!mailsToUser.Success) return BadRequest(mailsToUser);
             return Ok(mailsToUser);
        }
        
        [HttpPost("CreateAdministratorMailToOneUser")]
        public async Task<IActionResult> CreateAdministratorMailToOneUser([FromBody] CreateAdministratorMessageRequestModel model, [FromRoute] int UserId)
        {
            var mailsToUser = await _administratorMessageService.CreateAdministratorMessageToUser(model,UserId);
            if(!mailsToUser.Success) return BadRequest(mailsToUser);
             return Ok(mailsToUser);
        }
        
        [HttpPut("UpdateMail/{MailId}")]
        public async Task<IActionResult> UpdateMail([FromBody] UpdateAdministratorMessageRequestModel model, [FromRoute] int MailId)
        {
            var mailsToUser = await _administratorMessageService.UpdateAdministratorMessage(model,MailId);
            if(!mailsToUser.Success) return BadRequest(mailsToUser);
             return Ok(mailsToUser);
        }
        [HttpGet("GetAllMails")]
        public async Task<IActionResult> GetAllMails()
        {
            var mailsToUser = await _administratorMessageService.Get();
            if(!mailsToUser.Success) return BadRequest(mailsToUser);
             return Ok(mailsToUser);
        }
        [HttpGet("GetMailsSentToUser")]
        public async Task<IActionResult> GetMailsSentToAUser()
        {
            var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var mailsToUser = await _administratorMessageService.GetAdminMessagesToAUser(UserId);
            if(!mailsToUser.Success) return BadRequest(mailsToUser);
             return Ok(mailsToUser);
        }
    }
}
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Controllers
{
   [ApiController] 
   [Route("api/[controller]")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IWebHostEnvironment _webhostEnvironment;

        public ApplicationUserController(IApplicationUserService applicationUserService,IWebHostEnvironment webhostEnvironment)
        {
            _applicationUserService = applicationUserService;
            _webhostEnvironment = webhostEnvironment;
        }
        [HttpPost("CreateApplicationUser")]
          public async Task<IActionResult> CreateApplicationUser([FromForm] ApplicationUserCreateRequestModel model)
            {
                var files = HttpContext.Request.Form;
                if(files.Count!=0)
                {
                    string PhotoDirectory = Path.Combine(_webhostEnvironment.ContentRootPath,"UserImages");
                     Directory.CreateDirectory(PhotoDirectory);
                     foreach (var file in files.Files)
                     {
                          FileInfo fileInfo= new FileInfo(file.FileName);
                          string userImage = "user" + Guid.NewGuid().ToString().Substring(0,7) + $"{fileInfo.Extension}";
                          string fullPath= Path.Combine(PhotoDirectory,userImage);
                          using(var fileStream= new FileStream(fullPath,FileMode.Create))
                          {
                              file.CopyTo(fileStream);
                          }
                          model.UserImage = fullPath;
                     }
                }
             var response = await  _applicationUserService.CreateApplicationUser(model);
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
            }
        [HttpPut("UpdateApplicationUser/{Id}")]
         public async Task<IActionResult> UpdateApplicationUser([FromBody] ApplicationUserUpdateRequestModel model, [FromRoute]  int Id)
         {
               var response = await  _applicationUserService.UpdateApplicationUser(model,Id);
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
         }
        [HttpGet("GetLoggedInApplicationUser")]
        [Authorize(Roles = "ApplicationUser")]
         public async Task<IActionResult> GetLoggedInApplicationUser()
           {
               var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
               var response = await  _applicationUserService.Get(userId);
              if (!response.Success)
               {
                 return BadRequest(response);
                }
             return Ok(response);
            }
        [HttpGet("GetAllApplicationUsers")]
         public async Task<IActionResult> GetAllApplicationUsers()
         {
               var response = await  _applicationUserService.Get();
              if (!response.Success)
               {
                 return BadRequest(response);
                }
             return Ok(response);
         }
        [HttpDelete("DeleteApplicationUser/{Id}")]
         public async Task<IActionResult> DeleteApplicationUsers( int Id)
         {
               var response = await  _applicationUserService.Delete(Id);
              if (!response)
               {
                 return BadRequest(response);
                }
             return Ok(response);
         }
         

    }
}
using System;
using System.IO;
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
  
    public class ApplicationAdministratorController: ControllerBase
    {
         private readonly IApplicationAdministratorService _applicationAdministratorService;
         private readonly IWebHostEnvironment _whostEnvironment;
         

        public ApplicationAdministratorController(IApplicationAdministratorService applicationAdministratorService,IWebHostEnvironment whe)
        {
            _applicationAdministratorService = applicationAdministratorService;
            _whostEnvironment= whe;
         
        }
        [HttpPost("CreateApplicationAdministrator")]
          // [Authorize(Roles ="ApplicationAdministrator")]

        public async Task<IActionResult> Create([FromForm]CreateApplicationAdministratorRequestModel model)
        {
           var files = HttpContext.Request.Form;
                if(files.Count!=0)
                {
                    string PhotoDirectory = Path.Combine(_whostEnvironment.ContentRootPath,"AdminImages");
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
                          model.AdminImage = fullPath;
                     }
                }
  
              var appAdministrator = await _applicationAdministratorService.CreateApplicationAdministrator(model);
              if(!appAdministrator.Success) return BadRequest(appAdministrator);
              return Ok(appAdministrator);

        }
        [HttpPost("CreateSubAdministrator")]
        [Authorize(Roles ="ApplicationAdministrator")]
        [Authorize(Roles ="ApplicationSubAdministrator")]
        public async Task<IActionResult> CreateSubAdministrator(CreateSubAdministratorRequestModel model)
        {
          
              var appAdministrator = await _applicationAdministratorService.CreateSubAdministrator(model);
              if(!appAdministrator.Success) return BadRequest(appAdministrator);
              return Ok(appAdministrator);

        }
         [HttpGet("GetApplicationAdministrator/{Id}")]
         public async Task<IActionResult> GetApplicationUser([FromRoute] int Id)
         {
               var response = await  _applicationAdministratorService.Get(Id);
              if (!response.Success)
               {
                 return BadRequest(response);
                }
             return Ok(response);
         }
        [HttpGet("GetAllApplicationAdministrators")]
         public async Task<IActionResult> GetAllApplicationAdministrators()
         {
               var response = await  _applicationAdministratorService.Get();
              if (!response.Success)
               {
                 return BadRequest(response);
                }
             return Ok(response);
         }
        [HttpDelete("DeleteApplicationAdministrator/{Id}")]
         [Authorize(Roles ="ApplicationAdministrator")]
         public async Task<IActionResult> DeleteApplicationAdmin(int Id)
         {
               var response = await  _applicationAdministratorService.Delete(Id);
              if (!response)
               {
                 return BadRequest(response);
                }
             return Ok(response);
         }
    }
}
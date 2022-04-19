using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Controllers
{
     [ApiController]
    [Route("api/[controller]")]

    public class PostLogController : ControllerBase
    {
        
        private readonly IPostLogService _postLogService;

        public PostLogController(IPostLogService posrLogService)
        {
            _postLogService = posrLogService;
        }

        [HttpPost("CreatePostLog")]
         public async Task<IActionResult> CreatePostLog([FromBody] CreatePostLogRequestModel model)
         {
             var response = await  _postLogService.CreatePostLog(model);
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
         }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageOfOriginController : ControllerBase
    {
         private readonly ILanguageOfOriginService _LanguageOfOriginService;

        public LanguageOfOriginController(ILanguageOfOriginService LanguageOfOriginService)
        {
            _LanguageOfOriginService = LanguageOfOriginService;
        }
        [HttpPost("CreateHardLanguageOfOrigin")]
        public async Task<IActionResult> CreateLanguageOfOrigin(CreateLanguageOfOriginRequestModel model)
        {
             var LanguageOfOrigin = await _LanguageOfOriginService.Create(model);
             if(!LanguageOfOrigin.Success) return BadRequest(LanguageOfOrigin);
             return Ok(LanguageOfOrigin);
        }
        [HttpGet("SearchLanguageOfOrigin/{SearchText}")]
        public async Task<IActionResult> SearchLanguageOfOrigin([FromRoute] string SearchText)
        {
           var LanguageOfOriginResult = await _LanguageOfOriginService.Search(SearchText);
           if(!LanguageOfOriginResult.Success) return BadRequest(LanguageOfOriginResult);
           return Ok(LanguageOfOriginResult);
        }
        [HttpGet("GetLanguageOfOrigin/{Id}")]
        public async Task<IActionResult> GetLanguageOfOrigin([FromRoute]  int Id)
        {
           var LanguageOfOriginResult = await _LanguageOfOriginService.Get(Id);
           if(!LanguageOfOriginResult.Success) return BadRequest(LanguageOfOriginResult);
           return Ok(LanguageOfOriginResult);
        }
        [HttpGet("GetAllLanguageOfOrigins")]
        public async Task<IActionResult> GetAllLanguageOfOrigins()
        {
           var LanguageOfOriginResult = await _LanguageOfOriginService.Get();
           if(!LanguageOfOriginResult.Success) return BadRequest(LanguageOfOriginResult);
           return Ok(LanguageOfOriginResult);
        }
        [HttpPut("UpdateLanguageOfOrigin/{Id}")]
        public async Task<IActionResult> UpdateLanguageOfOrigin([FromBody]UpdateLanguageOfOriginRequestModel model,[FromRoute] int Id)
        {
           var LanguageOfOriginResult = await _LanguageOfOriginService.Update(model,Id);
           if(!LanguageOfOriginResult.Success) return BadRequest(LanguageOfOriginResult);
           return Ok(LanguageOfOriginResult);
        }
    }
}
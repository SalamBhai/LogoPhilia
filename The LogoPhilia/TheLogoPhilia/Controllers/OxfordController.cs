using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Interfaces.IServices;

namespace TheLogoPhilia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OxfordController : ControllerBase
    {
        private readonly IOxfordService _oxfordService;

        public OxfordController(IOxfordService oxfordService)
        {
            _oxfordService = oxfordService;
        }
        [HttpGet("ConnectToOxford")]
        public IActionResult ConnectOxford(string word)
        {
            var result = _oxfordService.ConnectToOxford(word);
           
            return Ok(result);
        }
        [HttpGet("ConnectToOxfordForAudio")]
        public IActionResult ConnectOxfordForAudio(string word)
        {
            var result = _oxfordService.ConnectToOxfordForAudio(word);
            
            return Ok(result);
        }
       
    }
}
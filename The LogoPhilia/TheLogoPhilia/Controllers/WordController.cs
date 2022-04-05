using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordController : ControllerBase
    {
        private readonly IWordService _wordService;

        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }
        [HttpPost("CreateHardWord")]
      //   [Authorize]
        public async Task<IActionResult> CreateWord(CreateWordRequestModel model)
        {
             var word = await _wordService.CreateWord(model);
             if(!word.Success) return BadRequest(word);
             return Ok(word);
        }
        [HttpGet("SearchWord/{SearchText}")]
        public async Task<IActionResult> SearchWord([FromRoute] string SearchText)
        {
           var wordResult = await _wordService.Search(SearchText);
           if(!wordResult.Success) return BadRequest(wordResult);
           return Ok(wordResult);
        }
        [HttpGet("GetWord/{Id}")]
        public async Task<IActionResult> GetWord([FromRoute] int WordId)
        {
           var wordResult = await _wordService.GetWord(WordId);
           if(!wordResult.Success) return BadRequest(wordResult);
           return Ok(wordResult);
        }
        [HttpGet("GetAllWords")]
        public async Task<IActionResult> GetAllWords()
        {
           var wordResult = await _wordService.GetAll();
           if(!wordResult.Success) return BadRequest(wordResult);
           return Ok(wordResult);
        }
        [HttpPut("UpdateWord/{Id}")]
        public async Task<IActionResult> UpdateWord([FromBody] UpdateWordRequestModel model,[FromRoute] int WordId)
        {
           var wordResult = await _wordService.UpdateWord(WordId,model);
           if(!wordResult.Success) return BadRequest(wordResult);
           return Ok(wordResult);
        }

    }
}
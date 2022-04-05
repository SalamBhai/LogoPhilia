using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace TheLogoPhilia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _NotesService;

        public NotesController(INotesService notesService)
        {
            _NotesService = notesService;
        }

        [HttpPost("CreateNotes")]
          [Authorize(Roles = "ApplicationUser")]
        public async Task<IActionResult> CreateNotes(CreateNotesRequestModel model)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _NotesService.Create(model, userId);
            if(response.Success==false) return BadRequest();
            return Ok(response);
        }
          [Authorize(Roles = "ApplicationUser")]
        [HttpPut("UpdateNote/{Id}")]
         public async Task<IActionResult> UpdateNote([FromBody] UpdateNotesRequestModel model, [FromRoute] int Id)
          {
             var response = await  _NotesService.Update(model,Id);
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
          }

         [HttpGet("GetNote/{Id}")]
         public async Task<IActionResult> GetNote([FromRoute] int Id)
          {
             var response = await  _NotesService.Get(Id);
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
          }
         [HttpGet("GetNotesOfAnApplicationUser")]
         [Authorize(Roles = "ApplicationUser")]
         public async Task<IActionResult> GetNotesOfAnApplicationUser()
          {
              var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
             var response = await  _NotesService.GetNotesByApplicationUser(userId);
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
          }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
         
         [HttpPost("CreateRole")]
         public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequestModel model)
         {
             var response = await  _roleService.Create(model);
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
         }
         [HttpPut("UpdateRole/{Id}")]
         public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequestModel model, [FromRoute] int Id)
          {
             var response = await  _roleService.Update(model,Id);
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
          }

         [HttpGet("GetRole/{Id}")]
         public async Task<IActionResult> GetRole([FromRoute] int Id)
          {
             var response = await  _roleService.Get(Id);
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
          }
         [HttpGet("AllRoles")]
         public async Task<IActionResult> GetALLRoleS()
          {
             var response = await  _roleService.Get();
             if (!response.Success)
             {
                 return BadRequest(response);
             }
             return Ok(response);
          }
         
    }
}
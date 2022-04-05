using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLogoPhilia.Authentication;
using TheLogoPhilia.Interfaces.IServices;
using TheLogoPhilia.Models;

namespace TheLogoPhilia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTTokenHandler _jwtTokenHandler;

        public UserController(IUserService userService, IJWTTokenHandler jwtTokenHandler)
        {
            _userService = userService;
            _jwtTokenHandler = jwtTokenHandler;
        }
        [HttpPost("UserLogIn")]
        public  async Task<IActionResult> UserLogin(LoginUserRequestModel model)
        {
            var loginRequest = await _userService.LoginApplicationUser(model);
            if(!loginRequest.Success) return BadRequest(loginRequest);
            var LoginResponseModel = new LoginResponseModel
            {
                Name = loginRequest.Data.ApplicationUserFullName,
                UserRoles = loginRequest.Data.UserRoles.ToList(),
                Email = loginRequest.Data.Email,
                Token = _jwtTokenHandler.GenerateToken(loginRequest.Data),
            };
            return Ok(LoginResponseModel);
        }
        [HttpPost("AdminLogIn")]
        public  async Task<IActionResult> AdminLogin(LoginAdministratorRequestModel model)
        {
            var loginRequest = await _userService.LoginApplicationAdministrator(model);
            if(!loginRequest.Success) return BadRequest(loginRequest);
            var LoginResponseModel = new LoginResponseModel
            {
                Name = loginRequest.Data.ApplicationUserFullName,
                UserRoles = loginRequest.Data.UserRoles.ToList(),
                Email = loginRequest.Data.Email,
                Token = _jwtTokenHandler.GenerateToken(loginRequest.Data),
            };
            return Ok(LoginResponseModel);
        }
    }
}
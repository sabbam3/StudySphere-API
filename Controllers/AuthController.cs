using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudySphere_API.Abstractions;
using StudySphere_API.Auth;
using StudySphere_API.Models.Authentication;
using StudySphere_API.Models.Entities;
using System.Runtime.CompilerServices;

namespace StudySphere_API.Controllers
{
    [ApiController]
    [Route("auth/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserService _userService;
        private readonly TokenGenerator _tokenGenerator;
        public AuthController(UserManager<UserEntity> userManager, TokenGenerator tokenGenerator, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] Register user)
        {
            if(await _userManager.FindByEmailAsync(user.Email) != null)
            {
                return BadRequest("User Already Exist");
            }
            if(!await _userService.CreateUserAsync(user))
            {
                return BadRequest("Can not create user, try again later");
            }
            else return Ok("User created successfully");
        }
        [HttpPost("log-in")]
        public async Task<IActionResult> LogInUserAsync([FromBody] LogIn user)
        {
            var entity = await _userManager.FindByEmailAsync(user.Email);
            var result = await _userManager.CheckPasswordAsync(entity, user.Password);
            if (!result)
            {
                return BadRequest("UserName or Password is incorrect");
            }
                else return Ok(await _tokenGenerator.GenerateToken(entity.Id.ToString(), user.Email));
            
            
        }
    }
}

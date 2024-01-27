using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudySphere_API.Abstractions;
using StudySphere_API.Models.Entities;

namespace StudySphere_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "api-lecturer", AuthenticationSchemes = "Bearer")]
    public class LecturerController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IStudyService _studyService;
        public LecturerController(UserManager<UserEntity> userManager, IStudyService studyService)
        {
            _userManager = userManager;
            _studyService = studyService;
        }
        [HttpPost("add-score")]
        public async Task<IActionResult> AddScore(int studentId, double score)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _studyService.AddScoreAsync(user.Email, studentId, score);
                return Ok("Score added succesfully");
            }
            else return BadRequest("Something went wrong");
        }
        [HttpGet("get-students-grade")]
        public async Task<IActionResult> GetStudentsGradeAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                return Ok(await _studyService.GetStudentsGradeAsync(user.Email));
            }
            else return BadRequest("Cant find students grade");
        }
    }
}

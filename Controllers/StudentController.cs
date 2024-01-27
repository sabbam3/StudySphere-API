using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudySphere_API.Abstractions;
using StudySphere_API.Models.Entities;
using System.Runtime.CompilerServices;

namespace StudySphere_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "api-student", AuthenticationSchemes = "Bearer")]
    public class StudentController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IStudyService _studyService;
        public StudentController(UserManager<UserEntity> userManager, IStudyService studyService)
        {
            _userManager = userManager;
            _studyService = studyService;
        }
        [HttpPost("add-subject-for-student")]
        public async Task<IActionResult> ChooseSubjectAsync(int subjectId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                await _studyService.ChooseSubjectAsync(subjectId, user.Email);
                return Ok("Subject added successfully");
            }
        }
        [HttpGet("get-subjects")]
        public async Task<IActionResult> GetSubjectsAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                return Ok(await _studyService.GetYourSubjectsAsync(user.Email));
            }
            else return BadRequest("User not found");
        }
        [HttpGet("get-grades")]
        public async Task<IActionResult> GetGradesAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                return Ok(await _studyService.GetStudentGradesAsync(user.Email));
            }
            else return BadRequest("User not found");
        }
    }
}

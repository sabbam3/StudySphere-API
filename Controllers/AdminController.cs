using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudySphere_API.Abstractions;
using StudySphere_API.Models.Entities;
using StudySphere_API.Models.Requests;

namespace StudySphere_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "api-admin", AuthenticationSchemes = "Bearer")]
    public class AdminController : ControllerBase
    {
        private readonly IStudyService _studyService;
        private readonly UserManager<UserEntity> _userManager;
        public AdminController(IStudyService studyService, UserManager<UserEntity> userManager)
        {
            _studyService = studyService;
            _userManager = userManager; 
        }
        [HttpPost("add-subject")]
        public async Task<IActionResult> AddSubjectAsync([FromBody] SubjectRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found");
            else
            {
                await _studyService.AddSubjectAsync(request);

                return Ok("Subject added successfully");
            }
        }
        [HttpPost("assign-subject-to-student")]
        public async Task<IActionResult> AssignSubjectAsync([FromBody] GradeRequest request)
        {
            await _studyService.AddGradeAsync(request);
            return Ok("Grade added successfully");
        }
    }
}

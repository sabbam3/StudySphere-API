using Microsoft.AspNetCore.Identity;
using StudySphere_API.Abstractions;
using StudySphere_API.Auth;
using StudySphere_API.Models.Authentication;
using StudySphere_API.Models.Entities;
using System.Text.RegularExpressions;

namespace StudySphere_API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IStudyService _service;
        
        public UserService(UserManager<UserEntity> userManager, IStudyService service)
        {
            _userManager = userManager;
            _service = service;
            
        }
        public async Task<bool> CreateUserAsync(Register user)
        {
                IsValidEmail(user);
                var entity = new UserEntity(); 
                entity.Email = user.Email;
                entity.UserName = user.Email;
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                var result = await _userManager.CreateAsync(entity, user.Password);
                if (result.Succeeded)
                {
                await AddRoleEntity(user, entity);
                return true;
                }
                else return false;
           
        }
        private async Task AddRoleEntity(Register user, UserEntity entity)
        {
            if (IsStudent(user.Email))
            {
                await _service.AddStudentAsync(user, entity);
                await _userManager.AddToRoleAsync(entity, "api-student");
            }
            if (IsLecturer(user.Email))
            {
                await _service.AddLecturerAsync(user, entity);
                await _userManager.AddToRoleAsync(entity, "api-lecturer");
            }
            if (IsAdmin(user.Email))
            {
                await _userManager.AddToRoleAsync(entity, "api-admin");
            }
        }
        private bool IsLecturer(string email)
        {
            return Regex.IsMatch(email, @"@lecturer\.com$", RegexOptions.IgnoreCase);
        }
        private bool IsStudent(string email)
        {
            return Regex.IsMatch(email, @"@student\.com$", RegexOptions.IgnoreCase);
        }
        private bool IsAdmin(string email)
        {
            return Regex.IsMatch(email, @"@admin\.com$", RegexOptions.IgnoreCase);
        }
        private void IsValidEmail(Register user)
        {
            if(user.Email != null)
            if (!(IsLecturer(user.Email) || IsStudent(user.Email) || IsAdmin(user.Email)))
            {
                throw new Exception("Your email is not valid");
            }
            
        }
    }
}

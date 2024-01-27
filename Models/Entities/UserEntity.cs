using Microsoft.AspNetCore.Identity;

namespace StudySphere_API.Models.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        
        public string? FirstName { get; set; }   
        public string? LastName {  get; set; }
    }
}

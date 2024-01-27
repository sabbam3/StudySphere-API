using StudySphere_API.Models.Authentication;

namespace StudySphere_API.Abstractions
{
    public interface IUserService
    {
       Task<bool> CreateUserAsync(Register user);
    }
}

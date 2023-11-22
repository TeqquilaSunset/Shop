using AuthenticatorAPI.Models;
using AuthenticatorAPI.Models.Request;
using Microsoft.AspNetCore.Identity;

namespace AuthenticatorAPI.Services
{
    public interface IUserServices
    {
        public Task<IdentityResult> RegisterUserAsync(RegisterRequest newRegister, string? role);
        public Task<IEnumerable<User>> GetAllAsync();
        public  Task<User> FindUserByEmailAsync(string email);
    }
}

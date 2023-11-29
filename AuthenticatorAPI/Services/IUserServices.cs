using AuthenticatorAPI.Models;
using AuthenticatorAPI.Models.Request;
using Microsoft.AspNetCore.Identity;

namespace AuthenticatorAPI.Services
{
    public interface IUserServices
    {
        public Task<IdentityResult> RegisterUserAsync(RegisterRequest newRegister, string? role);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<IEnumerable<string>> GetRoleByUsernameAsync(string username);
        public  Task<User> FindUserByEmailAsync(string email);
        public  Task<User> FindUserByUsernameAsync(string username);
        public Task AddRoleForUser(string username, string role);
        public Task UpdateRoleForUser(string username, List<string> newRoles);
    }
}

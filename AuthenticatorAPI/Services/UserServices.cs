
using AuthenticatorAPI.Models;
using AuthenticatorAPI.Models.Request;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticatorAPI.Services
{
    /// <summary>
    /// Сервис позволяющий осуществлять основные операции с пользователем
    /// </summary>
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<User> _roleManager;
        private readonly IMapper _mappingProfile;
        public UserServices(UserManager<User> userManager, IMapper mappingProfile)
        {
            _userManager = userManager;
            _mappingProfile = mappingProfile;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterRequest newRegister, string? role)
        {
            var newUser = _mappingProfile.Map<User>(newRegister);
            var result = await _userManager.CreateAsync(newUser, newRegister.Password);
            if(!result.Succeeded)
            {
                return result;
            }

            if (role == null) 
            {
                role = "User";
            }
            var user = await _userManager.FindByNameAsync(newUser.UserName);
            await _userManager.AddToRoleAsync(user, role);
            return result;
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

    }
}

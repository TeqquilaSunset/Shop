
using AuthenticatorAPI.Models;
using AuthenticatorAPI.Models.Request;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthenticatorAPI.Services
{
    /// <summary>
    /// Сервис позволяющий осуществлять основные операции с пользователем
    /// </summary>
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mappingProfile;
        public UserServices(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mappingProfile)
        {
            _userManager = userManager;
            _mappingProfile = mappingProfile;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterRequest newRegister, string? role)
        {
            var newUser = _mappingProfile.Map<User>(newRegister);
            var result = await _userManager.CreateAsync(newUser, newRegister.Password);
            if (!result.Succeeded)
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

        public async Task AddRoleForUser(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);

            //Проверка существования роли
            if (!await _roleManager.RoleExistsAsync(role))
            {
                throw new ApplicationException($"Role {role} does not exist.");
            }

            // Проверка присвоения роли
            if (!await _userManager.IsInRoleAsync(user, role))
            {
                var result = await _userManager.AddToRoleAsync(user, role);
            }
            return;
        }

        public async Task<User> FindUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<IEnumerable<string>> GetRoleByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles;
        }

        public async Task UpdateRoleForUser(string username, List<string> newRoles)
        {
            var user = await _userManager.FindByNameAsync(username);
            var existingRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = newRoles.Except(existingRoles);
            var rolesToRemove = existingRoles.Except(newRoles);
            await _userManager.AddToRolesAsync(user, rolesToAdd);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return;
        }
    }
}

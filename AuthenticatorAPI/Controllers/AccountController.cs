using AuthenticatorAPI.Models;
using AuthenticatorAPI.Models.Dto;
using AuthenticatorAPI.Models.Request;
using AuthenticatorAPI.Services;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace AuthenticatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Объект для управления пользователями в identity
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        /// Сервис для проведения операция с пользователями
        /// </summary>
        private readonly IUserServices _userServices;
        /// <summary>
        /// Конфигурация для доступа к секрутному ключу в appsetting
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Сервис для работыы с jwt и refresh токенами
        /// </summary>
        private readonly TokenService _tokenService;
        /// <summary>
        /// Объект для управления авторизация в identity
        /// </summary>
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, IUserServices userServices,
            IConfiguration configuration, TokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _userServices = userServices;
            _configuration = configuration;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Метод регистрации нового пользователя
        /// </summary>
        /// <param name="register">Принимает объект RegisterRequest</param>
        /// <response code="200"> Регистрация успешно произошла </response>
        /// <response code="400">Регистрация не прошла, некорректный запрос</response>
        [AllowAnonymous]
        [HttpPost("registr")]
        public async Task<IActionResult> Register(Models.Request.RegisterRequest register)
        {
            var result = await _userServices.RegisterUserAsync(register, "Admin");

            if (result.Succeeded)
            {
                return Ok("Register successful");
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Метод аутентификации пользователя.
        /// </summary>
        /// <param name="newLogin">Принимает объект UserLoginRequest с данными для входа.</param>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest newLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(newLogin.Username, newLogin.Password, false, lockoutOnFailure: false);
            var user = await _userManager.FindByNameAsync(newLogin.Username);

            if (result.Succeeded)
            {
                var accessToken = _tokenService.CreateToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();
                await _tokenService.SetRefreshToken(user, refreshToken);

                Response.Cookies.Append("auth_access_token", accessToken);
                Response.Cookies.Append("refreshToken", refreshToken.Token);
                return Ok(new { Message = "Login successful", AccessToken = accessToken, RefreshToken = refreshToken.Token });
            }

            if (result.IsLockedOut)
            {
                return BadRequest("Account bocked");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Метод получения списка всех пользователей.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userServices.GetAllAsync());
        }
        
        /// <summary>
        /// Получение списка ролей пользователя
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("roles/{username}")]
        public async Task<IActionResult> GetRoleByUsername(string username)
        {
            var roles = await _userServices.GetRoleByUsernameAsync(username);
            return Ok(roles);
        }

        /// <summary>
        /// Метод удаления всех пользователей.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("users")]
        public async Task<IActionResult> DeletAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }
            return Ok("All users have been successfully deleted");
        }

        /// <summary>
        /// Добавление роли пользователю
        /// </summary>
        /// <param name="newRoleDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRoleForUser(AddRoleDto newRoleDto)
        {
            await _userServices.AddRoleForUser(newRoleDto.UserName, newRoleDto.RoleName);
            return Ok();
        }

        /// <summary>
        /// Обновление ролей пользователя
        /// </summary>
        /// <param name="newRoleDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRoleUser(UpdateRolesDto newRoleDto)
        {
            await _userServices.UpdateRoleForUser(newRoleDto.Username, newRoleDto.Roles);
            return Ok();
        }



        /// <summary>
        /// Метод обновления токена доступа по токену обновления.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel token)
        {
            //var accessToken = Request.Cookies["auth_access_token"];
            //var refreshToken = Request.Cookies["refreshToken"];
            
            //if(accessToken == null || refreshToken == null)
            //{
            //    accessToken = tokenModel.AccessToken; 
            //    refreshToken = tokenModel.RefreshToken;
            //}

            var principal = _tokenService.GetPrincipalFromExpiredToken(_configuration, token.AccessToken);
            var username = principal.Identity.Name;

            var user = await _userServices.FindUserByUsernameAsync(username);
            if (user == null) return BadRequest();

            //Проверка валидности рефреш токена
            if (token.RefreshToken != user.RefreshToken || DateTime.Now > user.TokenExpires)
            {
                return Unauthorized();
            }

            var newAccessToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            await _tokenService.SetRefreshToken(user, newRefreshToken);

            Response.Cookies.Append("auth_access_token", newAccessToken);
            Response.Cookies.Append("refreshToken", newRefreshToken.Token);

            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }
    }
}

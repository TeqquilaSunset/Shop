using System.ComponentModel.DataAnnotations;

namespace AuthenticatorAPI.Models.Dto
{
    /// <summary>
    /// Запрос для авторизации
    /// </summary>
    public class UserLoginRequest
    {
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;

    }
}
